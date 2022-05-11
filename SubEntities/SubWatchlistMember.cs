using System;
using System.Collections.Generic;
using SmartfaceSolution.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using SmartfaceSolution.Entities;

namespace SmartfaceSolution.SubEntities
{
    /// <summary>
    /// <c>SubWatchlistMember</c> provide all the operation that are necessary to operate in the WatchlistMember entity 
    /// </summary>
    public class SubWatchlistMember
    {
        
        /// <summary>
        /// Method <c>createWatchListMember</c> create a new watchlistMember in the system
        /// </summary>
        /// <param name="displayName">the display name of the watchlistMember</param>
        /// <param name="fullName">the full name of the watchlistMember</param>
        /// <param name="note">the note that that contain watchlistmember email, phoneNumber, and id </param>
        /// <returns>the new watchlistMember</returns>
        public WatchlistMember createWatchListMember(string displayName, string fullName, string note)
        {
            WatchlistMember watchlistMember = new WatchlistMember()
            {
                displayName = displayName, fullName = fullName, note = note
            };
            string result = new SmartfaceResquest().requestWithBody("WatchlistMembers", "POST",
                JsonSerializer.Serialize(watchlistMember));
            watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);
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
        public WatchlistMember updateWatchListMember(string id, string displayName, string fullName,
            string note)
        {
            WatchlistMember watchlistMember = new WatchlistMember()
            {
                id = id, displayName = displayName, fullName = fullName, note = note
            };

            string result = new SmartfaceResquest().requestWithBody("WatchlistMembers", "PUT",
                JsonSerializer.Serialize(watchlistMember));
            watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(result);

            return watchlistMember;
        }

        /// <summary>
        /// Method <c>deleteWatchListMember</c> delete a specific watchListMember from the system 
        /// </summary>
        /// <param name="id">the watchlistMemebr id</param>
        /// <returns>the response of the deleting</returns>
        public string deleteWatchListMember(String id)
        {
            return new SmartfaceResquest().requestNoBody("WatchlistMembers/" + id, "DELETE");
        }

        /// <summary>
        /// Method <c>registerNewMember</c> register a new member in a specific watchlist
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <param name="watchlistId">the watchlist id</param>
        /// <param name="imgUrl"></param>
        /// <returns>the registration response</returns>
        public string registerNewMember(string id, string watchlistId, string imageData)
        {
            string data = "{" +
                          "\"id\":\"" + id + "\",\"images\": [" + "{" + "\"data\":\"" + imageData + "\"" + "}" +
                          "],"
                          + "\"watchlistIds\":[" + "\"" + watchlistId + "\"" + "]" + "}";
            string resp = new SmartfaceResquest().requestWithBody("WatchlistMembers/Register", "POST", data);
            return resp;
        }

        /// <summary>
        /// Method <c>getWatchlistMember</c> retrieves a specific watchlistMember from the system 
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <returns>the watchlistMember</returns>
        public WatchlistMember getWatchlistMember(string id)
        {
            string resp = new SmartfaceResquest().requestNoBody("WatchlistMembers/" + id, "GET");
            WatchlistMember watchlistMember = JsonSerializer.Deserialize<WatchlistMember>(resp);
            return watchlistMember;
        }

        /// <summary>
        /// Method <c>getFaces</c> retrieves all the faces that in the system for a specific watchlistMember
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <returns>the MemberFaces</returns>
        public List<string> getFaces(string id)
        {
            List<string> images = new List<string>();
            string resp =
                new SmartfaceResquest().requestNoBody("WatchlistMembers/" + id.Trim() + "/Faces?PageSize=500",
                    "GET");
            MemberFaces faces = JsonSerializer.Deserialize<MemberFaces>(resp);
            for (int i = 0; i < faces.items.Count; i++)
            {
                images.Add(retrievesImage(faces.items[i].imageDataId));
            }
            return images;
        }

        /// <summary>
        /// Method <c>getMemberFace</c> retrieves the first added face of the watchlistMember
        /// </summary>
        /// <param name="id">the watchlistMember id</param>
        /// <returns>the MemberFaces</returns>
        public string getMemberFace(string id)
        {
            string resp =
                new SmartfaceResquest().requestNoBody(
                    "WatchlistMembers/" + id.Trim() + "/Faces?Ascending=true&PageSize=1", "GET");
            MemberFaces faces = JsonSerializer.Deserialize<MemberFaces>(resp);
            string image = retrievesImage(faces.items[faces.items.Count - 1].imageDataId);
            return image;
        }

        /// <summary>
        /// Method <c>retrievesAllWatchlistMembers</c> retrieves all the watchlistMembers in the system 
        /// </summary>
        /// <returns>the Members</returns>
        public Members retrievesAllWatchlistMembers()
        {
            string resp = new SmartfaceResquest().requestNoBody("WatchlistMembers?PageSize=500", "GET");

            Members watchlistMembers = JsonSerializer.Deserialize<Members>(resp);

            return watchlistMembers;
        }

        /// <summary>
        /// Method <c>retrievesImage</c> retrieves the face images from the system 
        /// </summary>
        /// <param name="imgId">the image id</param>
        /// <returns>the image</returns>
        public string retrievesImage(string imgId)
        {
            string image = "";
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData("http://localhost:8098/api/v1/Images/" + imgId);
                image = Convert.ToBase64String(data);
            }
            return image;
        }
    }
}