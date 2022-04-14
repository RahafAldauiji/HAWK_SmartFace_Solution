namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>Camera</c> provide all the attributes
    /// that needed in the camera entity.
    /// </summary>
    
    public class Camera
    {
        public string id { get; set; }
        public string name { get; set; }
        public string source { get; set; }
        public bool enabled { get; set; }
        public string faceDetectorResourceId { get; set; }
        public string pedestrianDetectorResourceId { get; set; }
        public string templateGeneratorResourceId { get; set; }
        public int redetectionTime { get; set; }
        public int templateGenerationTime { get; set; }
        public string trackMotionOptimization { get; set; }
        public string faceSaveStrategy { get; set; }
        public string maskImagePath { get; set; }
        public bool saveFrameImageData { get; set; }
        public int imageQuality { get; set; }
        public bool mpeG1PreviewEnabled { get; set; }
        public int mpeG1PreviewPort { get; set; }
        public int mpeG1VideoBitrate { get; set; }
        public int previewMaxDimension { get; set; }
        public FaceDetectorConfig faceDetectorConfig { get; set; }
        public string serviceName { get; set; }
        public string[] spoofDetectorResourceIds { get; set; }
    }

    public class FaceDetectorConfig
    {
        public int minFaceSize { get; set; }
        public int maxFaceSize { get; set; }
        public int maxFaces { get; set; }
        public int confidenceThreshold { get; set; }
    }
}