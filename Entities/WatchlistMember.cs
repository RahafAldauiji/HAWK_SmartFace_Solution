
namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>Members</c> has  all the members that are stored in the database
    /// </summary>
    public class Members
    {
        public WatchlistMember[] items { get; set; }
    }

    /// <summary>
    /// Entity <c>WatchlistMember</c> is one of the main classes in our system, it will provide all the attribute that needed in WatchlistMember entity
    /// The WatchlistMember information is stored in the database and will be fetched as json 
    /// </summary>
    public class WatchlistMember
    {

        public string displayName { get; set; }

        public string fullName { get; set; }

        public string note { get; set; }

        public string id { get; set; }

    }
}