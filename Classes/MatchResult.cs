namespace SmartfaceSolution.Classes
{
    public class MatchResult
    {
        private int score;
        private string watchlistMemberId;
        private string displayName;
        private string fullName;
        private string watchlistFullName; 
        private string watchlistDisplayName;
        private string watchlistId;

        public int Score
        {
            get => score;
            set => score = value;
        }

        public string WatchlistMemberId
        {
            get => watchlistMemberId;
            set => watchlistMemberId = value;
        }

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

        public string WatchlistFullName
        {
            get => watchlistFullName;
            set => watchlistFullName = value;
        }

        public string WatchlistDisplayName
        {
            get => watchlistDisplayName;
            set => watchlistDisplayName = value;
        }

        public string WatchlistId
        {
            get => watchlistId;
            set => watchlistId = value;
        }
    }
}