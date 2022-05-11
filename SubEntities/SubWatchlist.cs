using System;
using SmartfaceSolution.Models;
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
        /// <summary>
        /// Method <c>createWatchList</c> create a new watchlist in the system
        /// </summary>
        /// <param name="displayName">the display name of the watchlist</param>
        /// <param name="fullName">the full name of the watchlist</param>
        /// <param name="threshold">the threshold of the watchlist</param>
        /// <returns>the new watchlist</returns>
        public Watchlist createWatchList(string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = new Watchlist()
            {
                displayName = displayName, threshold = threshold, fullName = fullName
            };

            string resp =
                new SmartfaceResquest().requestWithBody("Watchlists", "POST", JsonSerializer.Serialize(watchlist));
            watchlist = JsonSerializer.Deserialize<Watchlist>(resp);

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
        public Watchlist updateWatchList(string id, string displayName, string fullName, int threshold)
        {
            Watchlist watchlist = new Watchlist()
            {
                displayName = displayName, id = id, fullName = fullName, threshold = threshold
            };

            string resp =
                new SmartfaceResquest().requestWithBody("Watchlists", "PUT", JsonSerializer.Serialize(watchlist));
            watchlist = JsonSerializer.Deserialize<Watchlist>(resp);

            return watchlist;
        }

        /// <summary>
        /// Method <c>deleteWatchList</c> delete a specific watchlist from the system 
        /// </summary>
        /// <param name="id">the id of the watchlist</param>
        /// <returns>the response of the deleting</returns>
        public string deleteWatchList(String id)
        {
            string resp = new SmartfaceResquest().requestWithBody("Watchlists/" + id, "DELETE", "");
            return resp;
        }

        /// <summary>
        /// Method <c>retrievesWatchlistMembers</c> will retrieves all the WatchlistMembers 
        /// </summary>
        /// <param name="watchlistId">the id of the watchlist</param>
        /// <returns>Members in the watchlist</returns>
        public Members retrievesWatchlistMembers(string watchlistId)
        {
            string resp =
                new SmartfaceResquest().requestNoBody("Watchlists/" + watchlistId + "/WatchlistMembers", "GET");
            Members watchlistMembers = JsonSerializer.Deserialize<Members>(resp);
            return watchlistMembers;
        }

        /// <summary>
        /// Method <c>retrievesAllWatchlist</c> will retrieves all the watchlist from the system
        /// </summary>
        /// <returns>list of watchlist</returns>
        public AllWatchlist retrievesAllWatchlist()
        {
            string resp = new SmartfaceResquest().requestNoBody("Watchlists", "GET");
            AllWatchlist watchlists = JsonSerializer.Deserialize<AllWatchlist>(resp);
            return watchlists;
        }

        /// <summary>
        /// Method <c>getWatchlist</c> retrieves a specific watchlist from the system 
        /// </summary>
        /// <param name="id">the watchlist id</param>
        /// <returns>the watchlist</returns>
        public Watchlist getWatchlist(string id)
        {
            string resp = new SmartfaceResquest().requestNoBody("Watchlists/" + id, "GET");
            Watchlist watchlist = JsonSerializer.Deserialize<Watchlist>(resp);
            return watchlist;
        }
    }
}