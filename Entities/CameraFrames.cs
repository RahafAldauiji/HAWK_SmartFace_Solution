namespace SmartfaceSolution.Entities
{
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