using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using SmartfaceSolution.Classes;

namespace SmartfaceSolution.SubClasses
{
    public class SubWatchlist : Watchlist
    {
        private string imgUrl = "C://SmartFaceImages//";
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

        public string request(string reqUrl, string methodType, string json)
        {
            string res = null;
            try
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/Watchlists" + reqUrl);
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

        public Watchlist createWatchList(string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = null;
            try
            {
                string json = "{\"displayName\":\"" + displayName + "\",\"fullName\":\"" + fullName +
                              "\",\"threshold\":" + threshold + "}";
                string resp = request("", "POST", json);
                watchlist = Newtonsoft.Json.JsonConvert.DeserializeObject<Watchlist>(resp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return watchlist;
        }

        public Watchlist updateWatchList(string id, string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = null;
            try
            {
                string json = "{ \"id\":\"" + id + "\",\"displayName\":\"" + displayName + "\",\"fullName\":\"" +
                              fullName + "\",\"threshold\":" + threshold + "}";
                string resp = request("", "PUT", json);

                watchlist = Newtonsoft.Json.JsonConvert.DeserializeObject<Watchlist>(resp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return watchlist;
        }

        public string deleteWatchList(String id)
        {
            string resp = null;
            try
            {
                resp = request("/" + id, "DELETE", "");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return resp;
        }

        public void retrievesAllWatchlists()
        {
            try
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/Watchlists");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                response(httpWebRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public string convertImageToString(string name)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(imgUrl+name);
            byte[] arrBytes;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                arrBytes = ms.ToArray();
            }

            return Convert.ToBase64String(arrBytes);
        }

        public void search()
        {
            string img = convertImageToString("R.jpg");
            string json = "{" + "\"image\":" + "{" + "\"data\":\"" + img + "\"" + "}" + "}";
            string resp = request("/Search", "POST", json);
            Console.WriteLine(resp);
        }
    }
}