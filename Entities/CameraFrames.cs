namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>CameraFrames</c> provide all the attribute that needed in CameraFrame
    /// The CameraFrame information is stored in the database and will be fetched as json 
    /// </summary>
    public class CameraFrames
    {
        
        public int totalItemCount { get; set; }

        public Frame[] items { get; set; }

        public int pageSize { get; set; }

        public int pageNumber { get; set; }

        public string previousPage { get; set; }

        public string nextPage { get; set; }
    }
}