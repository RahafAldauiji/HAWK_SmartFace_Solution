using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using SmartfaceSolution.Entities;
using SmartfaceSolution.SubEntities;
using SmartfaceSolution.Test;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SmartfaceSolution.MatchScop
{
    public class MatchService : IMatchService
    {
        public string response(HttpWebRequest httpWebRequest)
        {
            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            string result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                try
                {
                    result = streamReader.ReadToEnd();
                }
                finally
                {
                    streamReader.Close();
                }
            }

            return result;
        }

        public string requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(reqUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = methodType;
            res = response(httpWebRequest);
            return res;
        }

        public string requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            var httpWebRequest =
                (HttpWebRequest) WebRequest.Create(reqUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = methodType;
            if (!methodType.Equals("DELETE"))
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    try
                    {
                        streamWriter.Write(json);
                    }
                    finally
                    {
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
            }

            res = response(httpWebRequest);
            return res;
        }


        public async Task<TestMember> matchFaces()
        {
            List<MatchFaces> match = null;
            TestMember _testMember = null;
            using (var subscriber = new SubscriberSocket())
            {
                subscriber.Connect("tcp://127.0.0.1:2406");
                //subscriber.Subscribe(Encoding.ASCII.GetBytes("matchResults.match"));
                subscriber.Subscribe("matchResults.match");
                var topic = subscriber.ReceiveFrameString();
                var matchHit = subscriber.ReceiveFrameString();
                _testMember = JsonSerializer.Deserialize<TestMember>(matchHit);
                if (_testMember.Type.Equals("Match"))
                {
                    Console.WriteLine("msg " + _testMember.WatchlistDisplayName);
                    WatchlistMember watchlistMember =
                        await new SubWatchlistMember().getWatchlistMember(_testMember.WatchlistMemberId);
                    string email = watchlistMember.note.Split(',')[0];
                    string phoneNumber = watchlistMember.note.Split(',')[1];
                    Message.Message message = new Message.Message(1, watchlistMember.displayName,
                        DateTime.Now.ToLocalTime().ToString());
                   // Console.WriteLine("Out of the method");
                     message.sendEmail(email);
                    //message.sendSMS(phoneNumber);
                   // Thread.Sleep(3000);
                }
                //message.sendSMS(phoneNumber);
                // match = JsonConvert.DeserializeObject<List<MatchFaces>>(matchHit);
                // for (int j = 0; j < match.Count; j++)
                // {
                //     for (int k = 0; k < match[j].matchResults.Length; k++)
                //     {
                //         WatchlistMember watchlistMember =
                //             await new SubWatchlistMember().getWatchlistMember(match[j].matchResults[k]
                //                 .watchlistMemberId);
                //         //match[j].matchResults[k].watchlistMemberId;
                //         string email = watchlistMember.note.Split(',')[0];
                //         string phoneNumber = watchlistMember.note.Split(',')[1];
                //         Message.Message message =
                //             new Message.Message(1, watchlistMember.displayName, DateTime.Now.ToLocalTime().ToString());
                //         message.sendEmail(email);
                //         //message.sendSMS(phoneNumber);
                //     }
                // }
            }

            Console.WriteLine("In Match Method");
            //List<MatchFaces> match = null;

            // DateTime dayTime = DateTime.Now.ToLocalTime();
            // string dayTime = "2021-12-23T00:52:08.713Z";
            // DateTime dayTime = DateTime.Parse("2021-12-23T00:52:08.713Z");
            // Console.WriteLine(dayTime.ToString());
            // string resp = requestNoBody("http://localhost:8098/api/v1/Frames?Ascending=false&PageSize=100",
            //      "GET");
            //  CameraFrames frames = JsonConvert.DeserializeObject<CameraFrames>(resp);
            //      //JsonSerializer.Deserialize<CameraFrames>(resp);
            //  DateTime frameDateTime;
            //  for (int i = 0; i < frames.items.Length; i++)
            //  {
            //      frameDateTime = DateTime.Parse(frames.items[i].createdAt);
            //      //Console.WriteLine("Now="+ dayTime+" Frame="+frameDateTime);
            //      if (frameDateTime.Hour == dayTime.Hour && frameDateTime.Minute == dayTime.Minute)
            //      {
            // Console.WriteLine("in");
            // using (WebClient webClient = new WebClient())
            // {
            //     byte[] data =
            //         webClient.DownloadData(
            //             "http://localhost:8098/api/v1/Images/" + frames.items[i].imageDataId);
            //     string img = Convert.ToBase64String(data);
            //     string json = "{" + "\"image\":" + "{" + "\"data\":\"" + img + "\"" + "}" +
            //                   "}";
            //     resp =  requestWithBody("http://localhost:8098/api/v1/Watchlists/Search",
            //         "POST",
            //         json);
            //     Console.WriteLine(resp);
            //     match = JsonConvert.DeserializeObject<List<MatchFaces>>(resp);
            //         //JsonSerializer.Deserialize<List<MatchFaces>>(resp);
            //     // send message
            //     for (int j = 0; j < match.Count; j++)
            //     {
            //         for (int k = 0; k < match[j].matchResults.Length; k++)
            //         {
            //             WatchlistMember watchlistMember =
            //                 await new SubWatchlistMember().getWatchlistMember(match[j].matchResults[k]
            //                     .watchlistMemberId);
            //             //match[j].matchResults[k].watchlistMemberId;
            //             string email = watchlistMember.note.Split(',')[0];
            //             string phoneNumber = watchlistMember.note.Split(',')[1];
            //             Message.Message message =
            //                 new Message.Message(1, watchlistMember.displayName, frameDateTime.ToString());
            //             message.sendEmail(email);
            //             //message.sendSMS(phoneNumber);
            //         }
            //     }

            //
            return _testMember;
        }
    }
}


//    return match;
//     }
//  }

public interface IMatchService
{
    public Task<TestMember> matchFaces();
}