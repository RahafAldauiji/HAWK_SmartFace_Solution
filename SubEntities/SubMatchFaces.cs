using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SmartfaceSolution.Entities;

namespace SmartfaceSolution.SubEntities
{
    public class SubMatchFaces : MatchFaces
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

        public async Task<string> requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            await Task.Run(() =>
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create(reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                res = response(httpWebRequest);
            });
            return res;
        }

        public async Task<string> requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            await Task.Run(() =>
            {
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
            });
            return res;
        }


        public async Task<List<MatchFaces>> matchFaces()
        {
            List<MatchFaces> match = null;
            await Task.Run(async () =>
            {
                DateTime dayTime = DateTime.Now.ToLocalTime();
                string resp = await requestNoBody("http://localhost:8098/api/v1/Frames?Ascending=false&PageSize=100",
                    "GET");
                CameraFrames frames = JsonSerializer.Deserialize<CameraFrames>(resp);
                DateTime frameDateTime;
                for (int i = 0; i < frames.items.Length; i++)
                {
                    frameDateTime = DateTime.Parse(frames.items[i].createdAt);
                    //Console.WriteLine("Now="+ dayTime+" Frame="+frameDateTime);
                    if (frameDateTime.Hour == dayTime.Hour && frameDateTime.Minute == dayTime.Minute)
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            byte[] data =
                                webClient.DownloadData(
                                    "http://localhost:8098/api/v1/Images/" + frames.items[i].imageDataId);
                            string img = Convert.ToBase64String(data);
                            string json = "{" + "\"image\":" + "{" + "\"data\":\"" + img + "\"" + "}" +
                                          "}";
                            resp = await requestWithBody("http://localhost:8098/api/v1/Watchlists/Search",
                                "POST",
                                json);
                            match = JsonSerializer.Deserialize<List<MatchFaces>>(resp);
                            // send message
                            // for (int j = 0; j < match.Count; j++)
                            // {
                                // for (int k = 0; k < match[j].matchResults.Length; k++)
                                // {
                                //     WatchlistMember watchlistMember =
                                //         await new SubWatchlistMember().getWatchlistMember(match[j].matchResults[k]
                                //             .watchlistMemberId);
                                //     //match[j].matchResults[k].watchlistMemberId;
                                //     string email = watchlistMember.note.Split(',')[0];
                                //     string phoneNumber = watchlistMember.note.Split(',')[1];
                                //     // Message.Message message =
                                //     //     new Message.Message(1, watchlistMember.displayName, frameDateTime.ToString());
                                //     // message.sendEmail(email);
                                //     //message.sendSMS(phoneNumber);
                                // }
                            //}

                            //
                            return match;
                        }
                    }
                }


                return match;
            });
            return match;
        }

        public static void matchSocket()
        {
            string ip = "127.0.0.1";
            int port = 80;
            var server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();
            Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);
            // Thread t = new Thread(SubMatchFaces.matchFaces);
        }
        // public string convertImageToString(string url)
        // {
        //     System.Drawing.Image img = System.Drawing.Image.FromFile(url);
        //
        //     byte[] arrBytes;
        //     using (var ms = new MemoryStream())
        //     {
        //         img.Save(ms, img.RawFormat);
        //         arrBytes = ms.ToArray();
        //     }
        //
        //     return Convert.ToBase64String(arrBytes);
        // }
        //
        //
        // public async Task<string> saveImage(string imgId)
        // {
        //     string image = "";
        //     await Task.Run(async() =>
        //     {
        //         try
        //         {
        //             using (WebClient webClient = new WebClient())
        //             {
        //                 byte[] data =  webClient.DownloadData("http://localhost:8098/api/v1/Images/" + imgId);
        //                 using (MemoryStream mem = new MemoryStream(data))
        //                 {
        //                     using (var yourImage = Image.FromStream(mem))
        //                     {
        //                         yourImage.Save("C://SmartFaceImages//" + imgId + ".Jpeg", ImageFormat.Jpeg);
        //                         image = Convert.ToBase64String(data);
        //                     }
        //                 }
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine(ex.Message);
        //         }
        //     });
        //     return image;
        // }
    }
}