using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using SmartfaceSolution.Classes;
using System.Text.Json;
using System.Threading.Tasks;

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
        public async Task<string> requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            await Task.Run(() =>
            {
                try
                {
                    var httpWebRequest =
                        (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/Watchlists/" + reqUrl);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = methodType;
                    res = response(httpWebRequest);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            });
            return res;
        }
        public async Task<string> request(string reqUrl, string methodType, string json)
        {
            string res = null;
            await Task.Run(() =>
            {
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
            });
            return res;
        }

        public async Task<Watchlist> createWatchList(string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = null;
            await Task.Run(async () =>
            {
                try
                {
                    string json = "{\"displayName\":\"" + displayName + "\",\"fullName\":\"" + fullName +
                                  "\",\"threshold\":" + threshold + "}";
                    string resp = await request("", "POST", json);
                    watchlist = JsonSerializer.Deserialize<Watchlist>(resp);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return watchlist;
        }

        public async Task<Watchlist> updateWatchList(string id, string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = null;
            await Task.Run(async() =>
            {
                try
                {
                    string json = "{ \"id\":\"" + id + "\",\"displayName\":\"" + displayName + "\",\"fullName\":\"" +
                                  fullName + "\",\"threshold\":" + threshold + "}";
                    string resp = await request("", "PUT", json);

                    watchlist = JsonSerializer.Deserialize<Watchlist>(resp);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return watchlist;
        }

        public async Task<string> deleteWatchList(String id)
        {
            string resp = null;
            await Task.Run(async() =>
            {
                try
                {
                    resp = await request("/" + id, "DELETE", "");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return resp;
        }

        public async Task retrievesAllWatchlists()
        {
            await Task.Run(async() =>
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
            });
        }

        public string convertImageToString(string name)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(imgUrl + name);
            byte[] arrBytes;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                arrBytes = ms.ToArray();
            }

            return Convert.ToBase64String(arrBytes);
        }

        public async Task search()
        {
            string img = convertImageToString("R.jpg");
            string json = "{" + "\"image\":" + "{" + "\"data\":\"" + img + "\"" + "}" + "}";
            string resp = await request("/Search", "POST", json);
            Console.WriteLine(resp);
        }
        public async Task<WatchlistMembers> retrievesWatchlistMembers(string watchlistId)
        {
            WatchlistMembers watchlistMembers = null;
            await Task.Run(async () =>
            {
                try
                {
                    string resp = await requestNoBody(watchlistId + "/WatchlistMembers", "GET");
                    //Console.WriteLine(resp);
                    watchlistMembers = JsonSerializer.Deserialize<WatchlistMembers>(resp);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return watchlistMembers;
        }
    }
}