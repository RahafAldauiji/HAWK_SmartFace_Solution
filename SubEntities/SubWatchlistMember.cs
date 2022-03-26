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
using SmartfaceSolution.Entities;

namespace SmartfaceSolution.SubEntities
{
    /// <summary>
    /// <c>SubWatchlistMember</c> provide all the operation that are necessary to operate in the WatchlistMember entity 
    /// </summary>
    public class SubWatchlistMember
    {
        private string imgUrl = "C://SmartFaceImages//";

        /// <summary>
        /// Method <c>convertImageToBase64String</c> will encoding the image into Base64 String
        /// </summary>
        /// <param name="name">name of the image in the SmartFaceImages folder</param>
        /// <returns>Base64 String</returns>
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

        /// <summary>
        /// Method <c>createWatchListMember</c> create a new watchlistMember in the system
        /// </summary>
        /// <param name="displayName">the display name of the watchlistMember</param>
        /// <param name="fullName">the full name of the watchlistMember</param>
        /// <param name="note">the note that that contain watchlistmember email, phoneNumber, and id </param>
        /// <returns>the new watchlistMember</returns>
        public async Task<WatchlistMember> createWatchListMember(string displayName, string fullName, string note)
        {
            WatchlistMember watchlistMember = null;
            await Task.Run(async () =>
            {
                string json = "{\"displayName\":\"" + displayName + "\",\"fullName\":\"" + fullName +
                              "\",\"note\":\"" +
                              note + "\"}";
                string result = await new HttpResquest().requestWithBody("WatchlistMembers", "POST", json);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);
            });
            return watchlistMember;
        }

        /// <summary>
        /// Method <c>updateWatchListMember</c> update update a specific updateWatchListMember from the system
        /// </summary>
        /// <param name="id"> watchlistMember id</param>
        /// <param name="displayName">the new display name of the watchlistMember</param>
        /// <param name="fullName">the new full name of the watchlistMember</param>
        /// <param name="note">the new note</param>
        /// <returns>the updated watchlistMember</returns>
        public async Task<WatchlistMember> updateWatchListMember(string id, string displayName, string fullName,
            string note)
        {
            WatchlistMember watchlistMember = null;
            await Task.Run(async () =>
            {
                string json = "{ \"id\":\"" + id + "\",\"displayName\":\"" + displayName +
                              "\",\"fullName\":\"" + fullName + "\",\"note\":\"" + note + "\"}";
                string result = await new HttpResquest().requestWithBody("WatchlistMembers", "PUT", json);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);
            });
            return watchlistMember;
        }

        /// <summary>
        /// Method <c>deleteWatchListMember</c> delete a specific watchListMember from the system 
        /// </summary>
        /// <param name="id">the watchlistMemebr id</param>
        /// <returns>the response of the deleting</returns>
        public async Task<string> deleteWatchListMember(String id)
        {
            string resp = null;
            await Task.Run(async () =>
            {
                resp = await new HttpResquest().requestNoBody("WatchlistMembers/" + id, "DELETE");
            });
            return resp;
        }
        
        

        /// <summary>
        /// Method <c>registerNewMember</c> register a new member in a specific watchlist
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <param name="watchlistId">the watchlist id</param>
        /// <param name="imgUrl"></param>
        /// <returns>the registration response</returns>
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
                resp = await new HttpResquest().requestWithBody("WatchlistMembers/Register", "POST", json);
            });
            return resp;
        }

        /// <summary>
        /// Method <c>getWatchlistMember</c> retrieves a specific watchlistMember from the system 
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <returns>the watchlistMember</returns>
        public async Task<WatchlistMember> getWatchlistMember(string id)
        {
            WatchlistMember watchlistMember = null;
            await Task.Run(async () =>
            {
                string resp = await new HttpResquest().requestNoBody("WatchlistMembers/" + id, "GET");
                //Console.WriteLine(resp);
                watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(resp);
                // Console.WriteLine(watchlistMember.id);
            });
            return watchlistMember;
        }
        /// <summary>
        /// Method <c>getFaces</c> retrieves all the faces that in the system for a specific watchlistMember
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <returns>the MemberFaces</returns>
        public async Task<List<string>> getFaces(string id)
        {
            MemberFaces faces = null;
            List<string> images = new List<string>();
            await Task.Run(async () =>
            {
                string resp =
                    await new HttpResquest().requestNoBody("WatchlistMembers/" + id.Trim() + "/Faces?PageSize=50",
                        "GET");
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
        /// <summary>
        /// Method <c>getMemberFace</c> retrieves the first added face of the watchlistMember
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <returns>the MemberFaces</returns>
        public async Task<string> getMemberFace(string id)
        {
            MemberFaces faces;
            string images = null;
            await Task.Run(async () =>
            {
                string resp =
                    await new HttpResquest().requestNoBody(
                        "WatchlistMembers/" + id.Trim() + "/Faces?Ascending=true&PageSize=1", "GET");
                faces = JsonSerializer.Deserialize<MemberFaces>(resp);
                images = await retrievesImage(faces.items[faces.items.Count - 1].imageDataId);
            });
            return images;
        }

        /// <summary>
        /// Method <c>retrievesAllWatchlistMembers</c> retrieves all the watchlistMembers in the system 
        /// </summary>
        /// <returns>the Members</returns>
        public async Task<Members> retrievesAllWatchlistMembers()
        {
            Members watchlistMembers = null;
            await Task.Run(async () =>
            {
                string resp = await new HttpResquest().requestNoBody("WatchlistMembers", "GET");
                //Console.WriteLine(resp);
                watchlistMembers = JsonSerializer.Deserialize<Members>(resp);
            });
            return watchlistMembers;
        }
        /// <summary>
        /// Method <c>retrievesImage</c> retrieves the face images from the system 
        /// </summary>
        /// <param name="imgId">the image id</param>
        /// <returns>the image</returns>
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
    }
}