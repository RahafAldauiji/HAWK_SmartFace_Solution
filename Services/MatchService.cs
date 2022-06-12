using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NetMQ;
using NetMQ.Sockets;
using SmartfaceSolution.Entities;
using SmartfaceSolution.Helpers;
using SmartfaceSolution.SubEntities;

namespace SmartfaceSolution.Services
{
    public interface IMatchService
    {
        public MemberMatch matchFaces();
    }

    public class MatchService : IMatchService
    {
        private readonly ServerConfig _serverName;

        public MatchService(IOptions<ServerConfig> serverConfig)
        {
            _serverName = serverConfig.Value;
        }

        /// <summary>
        /// Method <c>sendNotification</c> will check the Notification table and store the timestamp of the detection 
        /// the method will check the member, camera position, and the time stamp
        /// if the user is has been detected in the range of 5 min and with the same camera position the system will not add a new record
        /// otherwise the system will will add the user id, camera position, message status, and timestamp in the notification table
        /// Moreover, the system will add the attendance in the attendance table
        /// </summary>
        /// <param name="member">The watchlistMember information that matched</param>
        /// <param name="memberMatch">The information of the match Results </param>
        /// <param name="cam">The information of the camera</param>
        private void sendNotification(WatchlistMember member, MemberMatch memberMatch, Camera cam)
        {
            //Database Connection 
            string sqlCommand;
            SqlCommand cmd;
            SqlDataReader dr = null;
            SqlConnection cnn;
            bool memberExist = false, camPosition = false, timeStampExist = false; 
            DateTime date =
                (new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(memberMatch.FrameTimestampMicroseconds / 1000))
                .ToLocalTime(); // the match timestamp converted from the Microseconds to local Datetime format 
            long frameDateTimeSseconds =
                new DateTimeOffset(date).ToUnixTimeSeconds(); // get the seconds of the frame
            int id = int.Parse(member.note.Split(',')[2]); // get the user id from the note in the WatchlistMember
            try
            {
                //open the connection with the database
                cnn = new SqlConnection(_serverName.DefaultConnection);
                cnn.Open();
                sqlCommand =
                    "SELECT * FROM [dbo].[Notification] WHERE MemberId = @memberId"; // sql select command to be executed
                cmd = new SqlCommand(sqlCommand, cnn);
                cmd.Parameters.Add("@memberId", System.Data.SqlDbType.VarChar, -1).Value = member.id;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        long notificationDateTimeSeconds =
                            new DateTimeOffset(DateTime.Parse((string) dr["TimeStamp"]))
                                .ToUnixTimeSeconds(); // get the seconds from the notification table
                        //Check if the member has been detected and stored in the Notification table in the range of 10 min
                        if (((string) (dr["MemberID"])).Trim().Equals(member.id.Trim())&& notificationDateTimeSeconds <= frameDateTimeSseconds &&
                            notificationDateTimeSeconds + 600 >= frameDateTimeSseconds) // 600 seconds = 10 min 
                        {
                            memberExist = true; // if the member has been detected before 
                            camPosition = (int) (dr["CamPosition"]) ==
                                          Int32.Parse(cam.name.Split("-")[1]
                                              .Trim()); // check if the cameras in the notification table and the match member positions are same
                            // check if the seconds od the detected member are same or in the range of 10 min with notification table
                            timeStampExist = true;
                        }
                    }
                }
            }
            finally
            {
                dr.Close();
            }

            try
            {
                // Notification table
                //insert the member in the notification table if the member not exists or different camera position or the different time
                if ((!memberExist || !camPosition || !timeStampExist) && date.Year != 1970)
                {
                    // SQL insert command to be executed
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
                    // // sending the detection notification message
                    Message.Message message = new Message.Message(cam.name.Split("-")[1].Trim().Equals("1") ? 1 : 2,
                        member.displayName,
                        date.ToString());
                    message.sendEmail(email); // sending the message using email 

                    // Attendance table 
                    // insert the member in the Attendance table if the camera position is enter camera
                    // otherwise update the record in the the Attendance table if the camera position is exit 
                    sqlCommand = Int32.Parse(cam.name.Split("-")[1]) == 1
                        ? "INSERT INTO [dbo].[Attendance] ([id], [EnterTime],[EnterDate]  ) VALUES (@id ,@time,@date )"
                        : " UPDATE [dbo].[Attendance] SET [ExitTime] = @time, [ExitDate]=@date  WHERE [id]=@id AND [EnterDate]= @date AND [ExitTime] IS NULL";
                    cmd = new SqlCommand(sqlCommand, cnn);
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                    cmd.Parameters.Add("@date", System.Data.SqlDbType.VarChar, -1).Value = date.ToString("MM/dd/yyyy");
                    cmd.Parameters.Add("@time", System.Data.SqlDbType.VarChar, -1).Value = date.ToString("HH:mm:ss");
                    int row = cmd.ExecuteNonQuery();
                    // if there is no record to be updated and the camera position is 2 for exit cam
                    if (row == 0 && Int32.Parse(cam.name.Split("-")[1]) == 2)
                    {
                        sqlCommand =
                            " INSERT INTO [dbo].[Attendance] ([id], [ExitTime],[ExitDate] ) VALUES (@id ,@time,@date )";
                        cmd = new SqlCommand(sqlCommand, cnn);
                        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                        cmd.Parameters.Add("@date", System.Data.SqlDbType.VarChar, -1).Value =
                            date.ToString("MM/dd/yyyy");
                        cmd.Parameters.Add("@time", System.Data.SqlDbType.VarChar, -1).Value =
                            date.ToString("HH:mm:ss");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            finally

            {
                cnn.Close();
            }
        }

        /// <summary>
        /// Method <c>matchFaces</c> check if there is a any match faces
        /// it listen to smartface notification API using ZeroMQ messaging library
        /// The ZeroMQ open a socket on TCP port 2406 for the communication 
        /// </summary>
        /// <returns>MemberMatch</returns>
        public MemberMatch matchFaces()
        {
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
                         new SubCamera().getCamera(memberMatch.StreamId); // get the camera from the stream id
                    if (memberMatch.Type.Equals("Match"))
                    {
                        if (memberMatch.WatchlistMemberId != null)
                        {
                            WatchlistMember watchlistMember =
                                 new SubWatchlistMember().getWatchlistMember(memberMatch.WatchlistMemberId);
                            sendNotification(watchlistMember, memberMatch, cam);
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