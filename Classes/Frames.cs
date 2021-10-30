namespace SmartfaceSolution.Classes
{
    public class Frames
    {
        #region variables

        private int totalItemCount;
        private Frame[] items;
        private int pageSize;
        private int pageNumber;
        private string previousPage;
        private string nextPage;

        #endregion

        #region methods

        public int TotalItemCount
        {
            get => totalItemCount;
            set => totalItemCount = value;
        }

        public Frame[] Items
        {
            get => items;
            set => items = value;
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value;
        }

        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = value;
        }

        public string PreviousPage
        {
            get => previousPage;
            set => previousPage = value;
        }

        public string NextPage
        {
            get => nextPage;
            set => nextPage = value;
        }

        #endregion
    }
}