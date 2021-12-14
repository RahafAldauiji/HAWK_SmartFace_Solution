using System.Collections.Generic;

namespace SmartfaceSolution.Entities
{
    // public class WatchlistMembers
    // {
    //     private List<WatchlistMember> items;
    //
    //     public List<WatchlistMember> Items
    //     {
    //         get => items;
    //         set => items = value;
    //     }
    // }

    public class Members
    {
        //sprivate WatchlistMember[] items;

        public WatchlistMember[] items { get; set; }
    }

    public class WatchlistMember
    {

        // private string displayName;
        // private string fullName;
        // private string note;
        // private string id;
        // private string createdAt;
        // private string updatedAt;
        // private Watchlist[] watchlist;
        // private Face[] faces;

 
        public string displayName { get; set; }

        public string fullName { get; set; }

        public string note { get; set; }

        public string id { get; set; }

    }
}