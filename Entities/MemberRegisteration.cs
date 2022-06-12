namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>MemberRegistration</c> provide all the attribute that needed for the Member Registration in the system
    /// </summary>
    public class MemberRegistration
    {
        public WatchlistMember watchlistMember { get; set; }
        public string watchlistId { get; set; }
        public string img { get; set; }
    }
}