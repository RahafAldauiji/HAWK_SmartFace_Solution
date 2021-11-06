using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SmartfaceSolution.Classes;

namespace SmartfaceSolution.SubClasses
{
    public class SubMatchFaces : MatchResult
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
            try
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create(reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                res = response(httpWebRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return res;
        }

        public string requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            try
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return res;
        }


        public List<MatchFaces> matchFaces()
        {
            List<MatchFaces> match = null;
            // await Task.Run(() =>
            //     {
            try
            {
                DateTime dayTime = DateTime.Now.ToLocalTime();
                string resp = requestNoBody("http://localhost:8098/api/v1/Frames?Ascending=false&PageSize=100", "GET");
                Frames frames = JsonSerializer.Deserialize<Frames>(resp);
                DateTime frameDateTime;
                for (int i = 0; i < frames.items.Length; i++)
                {
                    frameDateTime = DateTime.Parse(frames.items[i].createdAt);
                    //Console.WriteLine("Now="+ dayTime+" Frame="+frameDateTime);
                    if (frameDateTime.Hour == dayTime.Hour && frameDateTime.Minute == dayTime.Minute)
                    {
                        Console.WriteLine("hi");
                        using (WebClient webClient = new WebClient())
                        {
                            byte[] data =
                                webClient.DownloadData(
                                    "http://localhost:8098/api/v1/Images/" + frames.items[i].imageDataId);
                            string img = Convert.ToBase64String(data);
                            string json = "{" + "\"image\":" + "{" + "\"data\":\"" + img + "\"" + "}" +
                                          "}";
                            resp = requestWithBody("http://localhost:8098/api/v1/Watchlists/Search",
                                "POST",
                                json);
                            match = JsonSerializer.Deserialize<List<MatchFaces>>(resp);
                            Console.WriteLine(match);
                            return match;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debug.WriteLine(e.ToString());
            }

            return match;
        }

        public string convertImageToString(string url)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(url);

            byte[] arrBytes;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                arrBytes = ms.ToArray();
            }

            return Convert.ToBase64String(arrBytes);
        }


        public string saveImage(string imgId)
        {
            string image = "";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData("http://localhost:8098/api/v1/Images/" + imgId);
                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            yourImage.Save("C://SmartFaceImages//" + imgId + ".Jpeg", ImageFormat.Jpeg);
                            image = Convert.ToBase64String(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return image;
        }
    }
}