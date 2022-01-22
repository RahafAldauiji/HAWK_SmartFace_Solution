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
        public void sendMessage(WatchlistMember member, MemberMatch memberMatch, Camera cam)
        {
            //Database Connection 
            string connetionString;
            string sqlCommand;
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cnn = null;
            bool m = false, c = false, t = false; // m=member, c=cam, t=time
            DateTime date =
                (new DateTime(1970, 1, 1) + (TimeSpan.FromMilliseconds(memberMatch.FrameTimestampMicroseconds / 1000)))
                .ToLocalTime();
            long frameDateTimeMilliseconds = new DateTimeOffset(date).ToUnixTimeMilliseconds();
            int id = 0;
            connetionString =
                "Data Source=LAPTOP-O3E4PDUK\\SFEXPRESS;" + //change the server name
                "Initial Catalog=HAWK;" +
                "User id=smartface;" +
                "Password=smartface;";
            try
            {
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                //Check if the member is in the Event Notification table 
                sqlCommand = @"SELECT * FROM  EventNotifications WHERE [id] = (SELECT MAX(id) FROM EventNotifications)";
                cmd = new SqlCommand(sqlCommand, cnn);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr.GetInt32(0);
                        if (dr.GetString(1).Trim().Equals(member.id.Trim()))
                        {
                            m = true;
                            if (dr.GetString(2).Trim().Equals(cam.name.Split("-")[1].Trim())) c = true;
                            if (long.Parse(dr.GetString(4)) <= frameDateTimeMilliseconds &&
                                long.Parse(dr.GetString(4)) + 180000 >= frameDateTimeMilliseconds)
                                t = true; // 180000 Milliseconds = 3 min 
                        }
                    }
                }

                dr.Close();
                //insert if the member not exists or different camera position or the different time
                if (!m || !c || !t)
                {
                    sqlCommand = @"INSERT INTO EventNotifications VALUES (" + (id + 1) + ",'" + member.id + "','" +
                                 cam.name.Split("-")[1].Trim() + "','" + date + "','" + frameDateTimeMilliseconds +
                                 "','" + 1 + "')";
                    cmd = new SqlCommand(sqlCommand, cnn);
                    cmd.ExecuteReader();
                    string email = member.note.Split(',')[0];
                    string phoneNumber = member.note.Split(',')[1];
                    // sending the message
                    Message.Message message = new Message.Message(cam.name.Split("-")[1].Trim().Equals("in") ? 1 : 2,
                        member.displayName,
                        DateTime.Now.ToLocalTime().ToString());
                    message.sendEmail(email);
                    //message.sendSMS(phoneNumber);
                }

                Console.WriteLine("DB Connection Open!");
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