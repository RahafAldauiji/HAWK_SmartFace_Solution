using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using SmartfaceSolution.Classes;

namespace SmartfaceSolution.SubClasses
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

        public string requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            try
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/WatchlistMembers" + reqUrl);
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

        public string request(string reqUrl, string methodType, string json)
        {
            string res = null;
            try
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return res;
        }

        public string convertImageToString(string name)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(imgUrl + name);
            byte[] arrBytes;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                arrBytes = ms.ToArray();
                Console.WriteLine(Convert.ToBase64String(arrBytes));
            }

            return Convert.ToBase64String(arrBytes);
        }

        public WatchlistMember createWatchListMember(string displayName, string fullName, string note)
        {
            WatchlistMember watchlistMember = null;
            try
            {
                string json = "{\"displayName\":\"" + displayName + "\",\"fullName\":\"" + fullName + "\",\"note\":\"" +
                              note + "\"}";
                string result = request("", "POST", json);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return watchlistMember;
        }

        public string linkWatchListMember(string watchlistId, string watchlistMembersIds)
        {
            string resp = null;
            try
            {
                string json = "{\"watchlistId\":\"" + watchlistId + "\",\"watchlistMembersIds\":[\"" +
                              watchlistMembersIds + "\"]}";
                resp = request("/LinkToWatchlist", "POST", json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return resp;
        }

        public WatchlistMember updateWatchListMember(string id, string displayName, string fullName, string note)
        {
            WatchlistMember watchlistMember = null;
            try
            {
                string json = "{ \"id\":\"" + id + "\",\"displayName\":\"" + displayName +
                              "\",\"fullName\":\"" + fullName + "\",\"note\":\"" + note + "\"}";
                string result = request("", "PUT", json);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);
                Console.WriteLine(watchlistMember.id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return watchlistMember;
        }

        public string deleteWatchListMember(String id)
        {
            string resp = null;
            try
            {
                resp = requestNoBody("/" + id, "DELETE");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return resp;
        }

        public void addFaceFromSystem(string memberId, string faceId)
        {
            // There is a problem with this method 
            try
            {
                string json = "{" + "\"faceId\":\"" + faceId + "\"" + "}";
                string resp = request("/" + memberId + "/AddFaceFromSystem", "POST", json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public string unlinkWatchListMember(string watchlistId, string watchlistMembersIds)
        {
            string resp = null;
            try
            {
                string json = "{\"watchlistId\":\"" + watchlistId + "\",\"watchlistMembersIds\":[\"" +
                              watchlistMembersIds + "\"]}";
                resp = request("/UnlinkFromWatchlist", "POST", json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return resp;
        }

        public Face addNewFace(string id, string imgUrl)
        {
            Face face = null;
            try
            {
                string imageData = convertImageToString(imgUrl);
                string json = "{" + "\"imageData\":" + "{" + "\"data\":\"" + imageData + "\"" + "}" + "}";
                string resp = request("/" + id + "/AddNewFace", "POST", json);
                face = JsonSerializer.Deserialize<Face>(resp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return face;
        }

        public string register(string id, string watchlistId, string imgUrl)
        {
            string resp = null;
            try
            {
                string imageData = convertImageToString(imgUrl);
                string json = "{" +
                              "\"id\":\"" + id + "\",\"images\": [" + "{" + "\"data\":\"" + imageData + "\"" + "}" +
                              "],"
                              + "\"watchlistIds\":[" + "\"" + watchlistId + "\"" + "]" + "}";
                resp = request("/Register", "POST", json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return resp;
        }

        public string removeFace(string id, string faceId)
        {
            string resp = null;
            try
            {
                string json = "{" + "\"faceId\":\"" + faceId + "\"" + "}";
                resp = request("/" + id + "/RemoveFace", "POST", json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return resp;
        }

        public void matchResults()
        {
            //http://localhost:8099/odata/MatchResults
            try
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create("http://localhost:8099/odata/MatchResults");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                response(httpWebRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public WatchlistMember getWatchlistMember(string id)
        {
            WatchlistMember watchlistMember = null;
            try
            {
                string resp = requestNoBody("/" + id, "GET");
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(resp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);
            }

            return watchlistMember;
        }

        public List<string> getFaces(string id)
        {
            MemberFaces faces;
            List<string> images = new List<string>();
            try
            {
                string resp = requestNoBody("/" + id.Trim() + "/Faces?PageSize=50", "GET");
                faces = JsonSerializer.Deserialize<MemberFaces>(resp);
                for (int i = 0; i < faces.items.Count; i++)
                {
                    images.Add(retrievesImage(faces.items[i].imageDataId));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.Message);
            }

            return images;
        }

        public string getMemberFace(string id)
        {
            MemberFaces faces;
            string images = null;
            try
            {
                string resp = requestNoBody("/" + id.Trim() + "/Faces?Ascending=true&PageSize=3", "GET");
                faces = JsonSerializer.Deserialize<MemberFaces>(resp);
                images = retrievesImage(faces.items[faces.items.Count-1].imageDataId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.Message);
            }

            return images;
        }


        public Members retrievesAllWatchlistMembers()
        {
            Members watchlistMembers = null;
            try
            {
                string resp = requestNoBody("", "GET");
                //Console.WriteLine(resp);
                watchlistMembers = JsonSerializer.Deserialize<Members>(resp);
                Console.WriteLine(watchlistMembers.items.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.Message);
            }

            return watchlistMembers;
        }

        public string retrievesImage(string imgId)
        {
            string image = "";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data =webClient.DownloadData("http://localhost:8098/api/v1/Images/" + imgId);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return image;
        }

        public byte[] cropImageFile(string imgId)
        {
            //string imgId = "bb820d72-a6d8-4900-9952-fc74c0256d72";
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData("http://localhost:8098/api/v1/Images/" + imgId);

                MemoryStream imgMemoryStream = new MemoryStream();
                System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(new MemoryStream(data));

                Bitmap bmPhoto = new Bitmap(150, 150, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                try
                {
                    grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, 150, 150),
                        0, 0, imgPhoto.Width, imgPhoto.Height, GraphicsUnit.Pixel);
                    bmPhoto.Save("C://SmartFaceImages//" + imgId + ".Jpeg", ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Debug.WriteLine(ex.Message);
                }

                return imgMemoryStream.GetBuffer();
            }
        }
    }
}