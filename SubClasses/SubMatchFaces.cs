﻿using System;
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

        public List<MatchResult> matchFaces()
        {
            List<MatchResult> match = new List<MatchResult>();
            MatchResult m;
            //string reqUrl, string methodType, string json
            // await Task.Run(() =>
            //     {

            string dayTime = DateTime.Now.ToString("yyyy-M-ddTHH:mm:ss.ffZ");
            //Thread.Sleep(2000);
            string resp = requestNoBody("http://localhost:8098/api/v1/Frames?Ascending=false", "GET");
            //string dayTime = "2021-10-17T22:19:25.437Z";
            string[] dayTimeNow = dayTime.Replace("Z", "").Split('T');
            string[] frameSplit1 = resp.Split('[', ']');
            string[] frameSplit2 = frameSplit1[1].Split("},");
            Frame[] frame = new Frame[frameSplit2.Length];
            string[] dayTimeFrame = null;
            string[] split1;
            string[] split2;
            // frameSplit2[frameSplit2.Length - 1] =
            //     frameSplit2[frameSplit2.Length - 1].Remove(frameSplit2.Length - 1);
            int time;
            for (int i = 0; i < frameSplit2.Length; i++)
            {
                if (!(i == frameSplit2.Length - 1))
                {
                    frameSplit2[i] += "}";
                }

                frame[i] = Newtonsoft.Json.JsonConvert.DeserializeObject<Frame>(frameSplit2[i]);
                dayTimeFrame = frame[i].CreatedAt.Replace("Z", "").Split('T');
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
                if (split1[0] + "" + split1[1] == split2[0] + "" + split2[1])
                {
                    using (WebClient webClient = new WebClient())
                    {
                        byte[] data =
                            webClient.DownloadData(
                                "http://localhost:8098/api/v1/Images/" + frame[i].ImageDataId);
                        string img = Convert.ToBase64String(data);
                        string json = "{" + "\"image\":" + "{" + "\"data\":\"" + img + "\"" + "}" +
                                      "}";
                        resp = requestWithBody("http://localhost:8098/api/v1/Watchlists/Search",
                            "POST",
                            json);
                        string[] matches = resp.Split("},{");
                        matches[matches.Length - 1] = matches[matches.Length - 1].Replace("]}", "");
                        for (int j = 0; j < matches.Length; j++)
                        {
                            string[] splitMatch1 = matches[j].Split("\"matchResults\":");
                            splitMatch1[1] = splitMatch1[1].Remove(splitMatch1[1].Length - 1).Replace("[", "");
                            match.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<MatchResult>(splitMatch1[1]));
                        }

                        return match;
                    }
                }
            }


            return match;

            // );
            //return match;
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