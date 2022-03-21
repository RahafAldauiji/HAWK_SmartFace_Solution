using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using SmartfaceSolution.Entities;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartfaceSolution.SubEntities
{
    /// <summary>
    /// <c>SubWatchlist</c> provide all the operation that are necessary to operate in the watchlist entity 
    /// </summary>
    public class SubWatchlist
    {
        private string imgUrl = "C://SmartFaceImages//";

        /// <summary>
        /// Method <c>createWatchList</c> create a new watchlist in the system
        /// </summary>
        /// <param name="displayName">the display name of the watchlist</param>
        /// <param name="fullName">the full name of the watchlist</param>
        /// <param name="threshold">the threshold of the watchlist</param>
        /// <returns>the new watchlist</returns>
        public async Task<Watchlist> createWatchList(string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = null;
            await Task.Run(async () =>
            {
                string json = "{\"displayName\":\"" + displayName + "\",\"fullName\":\"" + fullName +
                              "\",\"threshold\":" + threshold + "}";
                string resp = await new HttpResquest().requestWithBody("Watchlists", "POST", json);
                watchlist = JsonSerializer.Deserialize<Watchlist>(resp);
            });
            return watchlist;
        }

        /// <summary>
        /// Method <c>updateWatchList</c> update update a specific watchlist from the system
        /// </summary>
        /// <param name="id"> id of the watchlist</param>
        /// <param name="displayName">the new display name of the watchlist</param>
        /// <param name="fullName">the new full name of the watchlist</param>
        /// <param name="threshold">the new threshold of the watchlist</param>
        /// <returns></returns>
        public async Task<Watchlist> updateWatchList(string id, string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = null;
            await Task.Run(async () =>
            {
                string json = "{ \"id\":\"" + id + "\",\"displayName\":\"" + displayName + "\",\"fullName\":\"" +
                              fullName + "\",\"threshold\":" + threshold + "}";
                string resp = await new HttpResquest().requestWithBody("Watchlists", "PUT", json);
                watchlist = JsonSerializer.Deserialize<Watchlist>(resp);
            });
            return watchlist;
        }
        /// <summary>
        /// Method <c>deleteWatchList</c> delete a specific watchlist from the system 
        /// </summary>
        /// <param name="id">the id of the watchlist</param>
        /// <returns>the response of the deleting</returns>
        public async Task<string> deleteWatchList(String id)
        {
            string resp = null;
            await Task.Run(async () => { resp = await new HttpResquest().requestWithBody("Watchlists/" + id, "DELETE", ""); });
            return resp;
        }
        /// <summary>
        /// Method <c>retrievesWatchlistMembers</c> will retrieves all the WatchlistMembers 
        /// </summary>
        /// <param name="watchlistId">the id of the watchlist</param>
        /// <returns>Members in the watchlist</returns>
        public async Task<Members> retrievesWatchlistMembers(string watchlistId)
        {
            Members watchlistMembers = null;
            await Task.Run(async () =>
            {
                string resp = await new HttpResquest().requestNoBody("Watchlists/"+watchlistId + "/WatchlistMembers", "GET");
                watchlistMembers = JsonSerializer.Deserialize<Members>(resp);
            });
            return watchlistMembers;
        }
        /// <summary>
        /// Method <c>retrievesAllWatchlist</c> will retrieves all the watchlist from the system
        /// </summary>
        /// <returns>list of watchlist</returns>
        public async Task<List<Watchlist>> retrievesAllWatchlist()
        {
            List<Watchlist> watchlists = null;
            await Task.Run(async () =>
            {
                string resp = await new HttpResquest().requestNoBody("Watchlists", "GET");
                watchlists = JsonSerializer.Deserialize<List<Watchlist>>(resp);
            });
            return watchlists;
        }
    }
}