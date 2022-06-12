namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>Watchlist</c> is one of the main classes in our system, it will provide all the attribute that needed in Watchlist entity
    /// The Watchlist information is stored in the database and will be fetched as json 
    /// </summary>
    public class Watchlist
    {
        public string displayName { get; set; }

        public string fullName { get; set; }

        public int threshold { get; set; }

        public string id { get; set; }

        public string createdAt { get; set; }

        public string updatedAt { get; set; }
    }

    /// <summary>
    /// Entity <c>AllWatchlist</c> has  all the watchlists that are stored in the database
    /// </summary>
    public class AllWatchlist
    {
        public Watchlist[] items { get; set; }
    }
}