namespace SmartfaceSolution.Classes
{
    public class Watchlist
    {
        #region variables
        private string displayName;
        private string fullName;
        private int threshold;
        private string id;
        private string createdAt;
        private string updatedAt;
        private WatchlistMember[] watchlistMembers;
        #endregion

        #region methods

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string FullName
        {
            get => fullName;
            set => fullName = value;
        }

        public int Threshold
        {
            get => threshold;
            set => threshold = value;
        }

        public string Id
        {
            get => id;
            set => id = value;
        }

        public string CreatedAt
        {
            get => createdAt;
            set => createdAt = value;
        }

        public string UpdatedAt
        {
            get => updatedAt;
            set => updatedAt = value;
        }
        #endregion
    }
}