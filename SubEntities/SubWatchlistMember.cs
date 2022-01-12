﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SmartfaceSolution.Entities;

namespace SmartfaceSolution.SubEntities
{
    public class SubWatchlistMember : WatchlistMember
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
                    //Console.WriteLine(result);
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
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/WatchlistMembers" + reqUrl);
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
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/WatchlistMembers" + reqUrl);
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

        public string convertImageToBase64String(string name)
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

        public async Task<WatchlistMember> createWatchListMember(string displayName, string fullName, string note)
        {
            WatchlistMember watchlistMember = null;
            await Task.Run(async () =>
            {
                string json = "{\"displayName\":\"" + displayName + "\",\"fullName\":\"" + fullName +
                              "\",\"note\":\"" +
                              note + "\"}";
                string result = await requestWithBody("", "POST", json);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);
            });
            return watchlistMember;
        }

        public async Task<string> linkWatchListMember(string watchlistId, string watchlistMembersIds)
        {
            string resp = null;
            await Task.Run(async () =>
            {
                string json = "{\"watchlistId\":\"" + watchlistId + "\",\"watchlistMembersIds\":[\"" +
                              watchlistMembersIds + "\"]}";
                resp = await requestWithBody("/LinkToWatchlist", "POST", json);
            });
            return resp;
        }

        public async Task<WatchlistMember> updateWatchListMember(string id, string displayName, string fullName,
            string note)
        {
            WatchlistMember watchlistMember = null;
            await Task.Run(async () =>
            {
                string json = "{ \"id\":\"" + id + "\",\"displayName\":\"" + displayName +
                              "\",\"fullName\":\"" + fullName + "\",\"note\":\"" + note + "\"}";
                string result = await requestWithBody("", "PUT", json);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);
            });
            return watchlistMember;
        }

        public async Task<string> deleteWatchListMember(String id)
        {
            string resp = null;
            await Task.Run(async () => { resp = await requestNoBody("/" + id, "DELETE"); });
            return resp;
        }

        // public async Task addFaceFromSystem(string memberId, string faceId)
        // {
        //     // There is a problem with this method 
        //     await Task.Run(async () =>
        //     {
        //         try
        //         {
        //             string json = "{" + "\"faceId\":\"" + faceId + "\"" + "}";
        //             string resp = await requestWithBody("/" + memberId + "/AddFaceFromSystem", "POST", json);
        //         }
        //         catch (Exception ex)
        //         {
        //             Debug.WriteLine(ex.Message);
        //         }
        //     });
        // }

        public async Task<string> unlinkWatchListMember(string watchlistId, string watchlistMembersIds)
        {
            string resp = null;
            await Task.Run(async () =>
            {
                string json = "{\"watchlistId\":\"" + watchlistId + "\",\"watchlistMembersIds\":[\"" +
                              watchlistMembersIds + "\"]}";
                resp = await requestWithBody("/UnlinkFromWatchlist", "POST", json);
            });
            return resp;
        }

        public async Task<Face> addNewFace(string id, string imgUrl)
        {
            Face face = null;
            await Task.Run(async () =>
            {
                string imageData = convertImageToBase64String(imgUrl);
                string json = "{" + "\"imageData\":" + "{" + "\"data\":\"" + imageData + "\"" + "}" + "}";
                string resp = await requestWithBody("/" + id + "/AddNewFace", "POST", json);
                face = JsonSerializer.Deserialize<Face>(resp);
            });
            return face;
        }

        public async Task<string> registerNewMember(string id, string watchlistId, string imgUrl)
        {
            string resp = null;
            await Task.Run(async () =>
            {
                string imageData = convertImageToBase64String(imgUrl);
                string json = "{" +
                              "\"id\":\"" + id + "\",\"images\": [" + "{" + "\"data\":\"" + imageData + "\"" + "}" +
                              "],"
                              + "\"watchlistIds\":[" + "\"" + watchlistId + "\"" + "]" + "}";
                resp = await requestWithBody("/Register", "POST", json);
            });
            return resp;
        }

        public async Task<string> removeFace(string id, string faceId)
        {
            string resp = null;
            await Task.Run(async () =>
            {
                string json = "{" + "\"faceId\":\"" + faceId + "\"" + "}";
                resp = await requestWithBody("/" + id + "/RemoveFace", "POST", json);
            });
            return resp;
        }

        // public void matchResults()
        // {
        //     //http://localhost:8099/odata/MatchResults
        //     try
        //     {
        //         var httpWebRequest = (HttpWebRequest) WebRequest.Create("http://localhost:8099/odata/MatchResults");
        //         httpWebRequest.ContentType = "application/json";
        //         httpWebRequest.Method = "GET";
        //         response(httpWebRequest);
        //     }
        //     catch (Exception ex)
        //     {
        //         Debug.WriteLine(ex.Message);
        //     }
        // }

        public async Task<WatchlistMember> getWatchlistMember(string id)
        {
            WatchlistMember watchlistMember = null;
            await Task.Run(async () =>
            {
                string resp = await requestNoBody("/" + id, "GET");
                //Console.WriteLine(resp);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(resp);
                // Console.WriteLine(watchlistMember.id);
            });
            return watchlistMember;
        }

        public async Task<List<string>> getFaces(string id)
        {
            MemberFaces faces = null;
            List<string> images = new List<string>();
            await Task.Run(async () =>
            {
                string resp = await requestNoBody("/" + id.Trim() + "/Faces?PageSize=50", "GET");
                //Console.WriteLine(resp);
                faces = JsonSerializer.Deserialize<MemberFaces>(resp);
                //Console.WriteLine(faces.items[0].age);
                for (int i = 0; i < faces.items.Count; i++)
                {
                    images.Add(await retrievesImage(faces.items[i].imageDataId));
                }
            });
            return images;
        }

        public async Task<string> getMemberFace(string id)
        {
            MemberFaces faces;
            string images = null;
            await Task.Run(async () =>
            {
                string resp = await requestNoBody("/" + id.Trim() + "/Faces?Ascending=true&PageSize=3", "GET");
                faces = JsonSerializer.Deserialize<MemberFaces>(resp);
                images = await retrievesImage(faces.items[faces.items.Count - 1].imageDataId);
            });
            return images;
        }


        public async Task<Members> retrievesAllWatchlistMembers()
        {
            Members watchlistMembers = null;
            await Task.Run(async () =>
            {
                string resp = await requestNoBody("", "GET");
                //Console.WriteLine(resp);
                watchlistMembers = JsonSerializer.Deserialize<Members>(resp);
            });
            return watchlistMembers;
        }

        public async Task<string> retrievesImage(string imgId)
        {
            string image = "";
            await Task.Run(() =>
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData("http://localhost:8098/api/v1/Images/" + imgId);
                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            var resizeImage = (Image) (new Bitmap(yourImage, new Size(120, 120)));
                            resizeImage.Save("C://SmartFaceImages//" + imgId + ".Jpeg", ImageFormat.Jpeg);
                            image = Convert.ToBase64String(data);
                            //Console.WriteLine(image);
                        }
                    }
                }
            });
            return image;
        }

        // public byte[] cropImageFile(string imgId)
        // {
        //     //string imgId = "bb820d72-a6d8-4900-9952-fc74c0256d72";
        //     using (WebClient webClient = new WebClient())
        //     {
        //         byte[] data = webClient.DownloadData("http://localhost:8098/api/v1/Images/" + imgId);
        //
        //         MemoryStream imgMemoryStream = new MemoryStream();
        //         System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(new MemoryStream(data));
        //
        //         Bitmap bmPhoto = new Bitmap(150, 150, PixelFormat.Format24bppRgb);
        //         bmPhoto.SetResolution(72, 72);
        //
        //         Graphics grPhoto = Graphics.FromImage(bmPhoto);
        //         grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //         grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //         grPhoto.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        //
        //         try
        //         {
        //             grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, 150, 150),
        //                 0, 0, imgPhoto.Width, imgPhoto.Height, GraphicsUnit.Pixel);
        //             bmPhoto.Save("C://SmartFaceImages//" + imgId + ".Jpeg", ImageFormat.Jpeg);
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine(ex.Message);
        //             Debug.WriteLine(ex.Message);
        //         }
        //
        //         return imgMemoryStream.GetBuffer();
        //     }
        // }
    }
}