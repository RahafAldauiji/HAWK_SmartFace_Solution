namespace SmartfaceSolution.Classes
{
    public class Frame
    {
        #region variables

        private string state;
        private string imageDataId;
        private string receivedAt;
        private int positionInMs;
        private string id;
        private string createdAt;
        private string updatedAt;

        #endregion

        #region methods

        public string State
        {
            get => state;
            set => state = value;
        }

        public string ImageDataId
        {
            get => imageDataId;
            set => imageDataId = value;
        }

        public string ReceivedAt
        {
            get => receivedAt;
            set => receivedAt = value;
        }

        public int PositionInMs
        {
            get => positionInMs;
            set => positionInMs = value;
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