namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>Watchlist</c> is one of the main classes in our system, it will provide all the attribute that needed in Watchlist entity
    /// The Watchlist information is stored in the database and will be fetched as json 
    /// </summary>
    public class Watchlist
    {
      
        // private string displayName;
        // private string fullName;
        // private int threshold;
        // private string id;
        // private string createdAt;
        // private string updatedAt;
        // private WatchlistMember[] watchlistMembers;

  
        public string displayName { get; set; }

        public string fullName { get; set; }

        public int threshold { get; set; }

        public string id { get; set; }

        public string createdAt { get; set; }

        public string updatedAt { get; set; }

    }
}