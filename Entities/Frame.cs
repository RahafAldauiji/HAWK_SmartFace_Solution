namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>Frame</c> is one of the main classes in our system, it will provide all the attribute that needed in frame entity
    /// The Frame information is stored in the database and will be fetched as json 
    /// </summary>
    public class Frame
    {
        public string state { get; set; }

        public string imageDataId { get; set; }

        public string receivedAt { get; set; }

        public int positionInMs { get; set; }

        public string id { get; set; }

        public string createdAt { get; set; }

        public string updatedAt { get; set; }
    }
}