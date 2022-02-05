namespace SmartfaceSolution.Entities
{
    
    public class Camera
    {

        //
        // private string id;
        // private string name;
        // private string source;
        // private bool enabled = true;
        // private FaceDetectorConfig faceDetectorConfig;
        // private string faceDetectorResourceId;
        // private SpoofDetectorConfig spoofDetectorConfig;
        // private PedestrianDetectorConfig pedestrianDetectorConfig;
        // private string pedestrianDetectorResourceId;
        // private string templateGeneratorResourceId;
        // private int redetectionTime;
        // private int templateGenerationTime;
        // private string trackMotionOptimization;
        // private string faceSaveStrategy = "FirstFace, BestFace";
        // private string maskImagePath;
        // private bool saveFrameImageData = true;
        // private int imageQuality;
        // private bool mpeG1PreviewEnabled = true;
        // private int mpeG1PreviewPort;
        // private int mpeG1VideoBitrate;
        // private int previewMaxDimension;
        // private string serviceName;
        // private string[] spoofDetectorResourceIds;


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

        public string serviceName { get; set; }

        public string[] spoofDetectorResourceIds { get; set; }
        //
        // public VideoFaceDetectorConfig faceDetectorConfig{ get; set; }
        
        //  public VideoPedestrianDetectorConfig pedestrianDetectorConfig{ get; set; }
        
        // public SpoofDetectorConfig spoofDetectorConfig { get; set; }
    }
}