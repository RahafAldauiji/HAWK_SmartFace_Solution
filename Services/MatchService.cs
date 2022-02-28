using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using SmartfaceSolution.Entities;
using SmartfaceSolution.SubEntities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SmartfaceSolution.Services
{
    public class MatchService : IMatchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberMatch"></param>
        /// <param name="cam"></param>
        public void sendMessage(WatchlistMember member, MemberMatch memberMatch, Camera cam)
        {
            //Database Connection 
            string connetionString;
            string sqlCommand;
            SqlCommand cmd;
            SqlDataReader dr = null;
            SqlConnection cnn = null;
            bool m = false, c = false, t = false; // m=member, c=cam, t=time
            DateTime date =
                (new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(memberMatch.FrameTimestampMicroseconds / 1000))
                .ToLocalTime();
            Console.WriteLine("date= " + date);
            long frameDateTimeMilliseconds = new DateTimeOffset(date).ToUnixTimeMilliseconds();
            Console.WriteLine("frameDateTimeMilliseconds = " + frameDateTimeMilliseconds);
            int id = int.Parse(member.note.Split(',')[2]);
            connetionString =
                "Data Source=LAPTOP-O3E4PDUK\\SFEXPRESS;" + //change the server name
                "Initial Catalog=HAWK;" +
                "User id=smartface;" +
                "Password=smartface;";
            try
            {
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                // The new section
                sqlCommand = "SELECT * FROM [dbo].[Notification] WHERE MemberId = @memberId";
                //Check if the member is in the Event Notification table 
                //sqlCommand = @"SELECT * FROM  Notification WHERE [id] = (SELECT MAX(id) FROM EventNotifications)";
                cmd = new SqlCommand(sqlCommand, cnn);
                cmd.Parameters.Add("@memberId", System.Data.SqlDbType.VarChar, -1).Value = member.id;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //id = (int) (dr["Id"]);
                        if (((string) (dr["MemberID"])).Trim().Equals(member.id.Trim()))
                        {
                            Console.WriteLine("Innnnnnnnnn");
                            m = true;
                            if (((int) (dr["CamPosition"])) == (Int32.Parse(cam.name.Split("-")[1]))) c = true;
                            DateTime nDateTime = DateTime.Parse((string) dr["TimeStamp"]);
                            long nDateTimeMilliseconds = new DateTimeOffset(nDateTime).ToUnixTimeMilliseconds();
                            if (nDateTimeMilliseconds <= frameDateTimeMilliseconds &&
                                nDateTimeMilliseconds + 180000 >= frameDateTimeMilliseconds)
                                t = true; // 180000 Milliseconds = 3 min 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dr.Close();
            }

            try
            {
                //insert if the member not exists or different camera position or the different time
                if (!m || !c || !t)
                {
                    Console.WriteLine("inside the if zeft");
                    sqlCommand =
                        "INSERT INTO [dbo].[Notification] ([Id], [MemberID] ,[CamPosition] ,[TimeStamp] ,[MessageStatus]) VALUES (@id ,@MemberID ,@CamPosition ,@TimeStamp ,@MessageStatus)";
                    cmd = new SqlCommand(sqlCommand, cnn);
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                    cmd.Parameters.Add("@MemberID", System.Data.SqlDbType.VarChar, -1).Value = member.id;
                    cmd.Parameters.Add("@CamPosition", System.Data.SqlDbType.Int, 1).Value =
                        Int32.Parse(cam.name.Split("-")[1]);
                    cmd.Parameters.Add("@TimeStamp", System.Data.SqlDbType.VarChar, -1).Value = date;
                    cmd.Parameters.Add("@MessageStatus", System.Data.SqlDbType.Int, 1).Value = 1;
                    cmd.ExecuteNonQuery();
                    string email = member.note.Split(',')[0];
                    string phoneNumber = member.note.Split(',')[1];
                    // // sending the message
                    Message.Message message = new Message.Message(cam.name.Split("-")[1].Trim().Equals("in") ? 1 : 2,
                        member.displayName,
                        date.ToUniversalTime().ToString());
                    message.sendEmail(email);
                    //message.sendSMS(phoneNumber);

                    // Attendance table 

                    if (Int32.Parse(cam.name.Split("-")[1]) == 1)
                    {
                        sqlCommand =
                            "INSERT INTO [dbo].[Attendance] ([id], [EnterTimeStamp]  ) VALUES (@id ,@EnterTimeStamp)";
                        cmd = new SqlCommand(sqlCommand, cnn);
                        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                        cmd.Parameters.Add("@EnterTimeStamp", System.Data.SqlDbType.VarChar, -1).Value = date;
                    }
                    else
                    {
                        sqlCommand =
                            " UPDATE [dbo].[Attendance] SET [ExitTimeStamp] = @ExitTimeStamp WHERE [id]=@id AND [ExitTimeStamp] IS NULL";
                        cmd = new SqlCommand(sqlCommand, cnn);
                        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                        cmd.Parameters.Add("@ExitTimeStamp", System.Data.SqlDbType.VarChar, -1).Value = date;
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public async Task<MemberMatch> matchFaces()
        {
            List<MatchFaces> match = null;
            MemberMatch memberMatch = null;
            try
            {
                using (var subscriber = new SubscriberSocket())
                {
                    //  listen to smartface notification API using ZeroMQ messaging library 
                    subscriber.Connect("tcp://127.0.0.1:2406");
                    subscriber.Subscribe("matchResults.match");
                    var topic = subscriber.ReceiveFrameString(); // matchResults
                    var matchHit = subscriber.ReceiveFrameString(); // This notification contains watchlist member
                    memberMatch = JsonSerializer.Deserialize<MemberMatch>(matchHit);
                    Camera cam =
                        await new SubCamera().getCamera(memberMatch.StreamId); // get the camera from the stream id
                    if (memberMatch.Type.Equals("Match"))
                    {
                        if (memberMatch.WatchlistMemberId != null)
                        {
                            WatchlistMember watchlistMember =
                                await new SubWatchlistMember().getWatchlistMember(memberMatch.WatchlistMemberId);
                            sendMessage(watchlistMember, memberMatch, cam);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return memberMatch;
        }
    }
}


public interface IMatchService
{
    public Task<MemberMatch> matchFaces();
}