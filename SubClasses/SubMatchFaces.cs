using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
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
            List<MatchFaces> match = new List<MatchFaces>();
            // await Task.Run(() =>
            //     {
            try
            {
                string dayTime = DateTime.Now.ToString("yyyy-M-ddTHH:mm:ss.ffZ");
                //Thread.Sleep(2000);
                string resp = requestNoBody("http://localhost:8098/api/v1/Frames?Ascending=false", "GET");
                //string dayTime = "2021-10-27T23:09:37.107Z";
                string[] dayTimeNow = dayTime.Replace("Z", "").Split('T');
                Frames frames = Newtonsoft.Json.JsonConvert.DeserializeObject<Frames>(resp);
                string[] dayTimeFrame = null;
                string[] split1;
                string[] split2;
                int time;
                for (int i = 0; i < frames.Items.Length; i++)
                {
                    //frame[i] = Newtonsoft.Json.JsonConvert.DeserializeObject<Frame>(frameSplit2[i]);
                    dayTimeFrame = frames.Items[i].CreatedAt.Replace("Z", "").Split('T');
                    split1 = dayTimeFrame[1].Split(":");
                    split2 = dayTimeNow[1].Split(":");
                    time = int.Parse(split1[0]) + 3;
                    if (time >= 24)
                    {
                        split1[0] = "";
                        time -= 24;
                        time += 0;
                    }

                    if (time < 10)
                        split1[0] = "0";

                    split1[0] += time + "";
                    // if (dayTimeNow[0]==(dayTimeFrame[0]))
                    // {
                    //Console.WriteLine(split1[0] + "" + split1[1] + "       " + split2[0] + "" + split2[1]);
                    if (split1[0] + "" + split1[1] == split2[0] + "" + split2[1])
                    {
                        Console.WriteLine("hi");
                        using (WebClient webClient = new WebClient())
                        {
                            byte[] data =
                                webClient.DownloadData(
                                    "http://localhost:8098/api/v1/Images/" + frames.Items[i].ImageDataId);
                            string img = Convert.ToBase64String(data);
                            string json = "{" + "\"image\":" + "{" + "\"data\":\"" + img + "\"" + "}" +
                                          "}";
                            resp = requestWithBody("http://localhost:8098/api/v1/Watchlists/Search",
                                "POST",
                                json);
                            match = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MatchFaces>>(resp);
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