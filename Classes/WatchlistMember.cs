using System.Collections.Generic;

namespace SmartfaceSolution.Classes
{
    public class WatchlistMembers
    {
        private List<WatchlistMember> items;

        public List<WatchlistMember> Items
        {
            get => items;
            set => items = value;
        }
    }
    public class WatchlistMember
    {
        #region variables

        private string displayName;
        private string fullName;
        private string note;
         private string id;
        // private string createdAt;
        // private string updatedAt;
        // private Watchlist[] watchlist;
        // private Face[] faces;

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

        public string Note
        {
            get => note;
            set => note = value;
        }

        public string Id
        {
            get => id;
            set => id = value;
        }

        // public string CreatedAt
        // {
        //     get => createdAt;
        //     set => createdAt = value;
        // }
        //
        // public string UpdatedAt
        // {
        //     get => updatedAt;
        //     set => updatedAt = value;
        // }

        #endregion
    }
}