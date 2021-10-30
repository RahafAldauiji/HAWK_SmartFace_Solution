namespace SmartfaceSolution.Classes
{
    public class Camera
    {
        #region variables

        private string id;
        private string name;
        private string source;
        private bool enabled = true;
        private FaceDetectorConfig faceDetectorConfig;
        private string faceDetectorResourceId;
        private SpoofDetectorConfig spoofDetectorConfig;
        private PedestrianDetectorConfig pedestrianDetectorConfig;
        private string pedestrianDetectorResourceId;
        private string templateGeneratorResourceId;
        private int redetectionTime;
        private int templateGenerationTime;
        private string trackMotionOptimization;
        private string faceSaveStrategy = "FirstFace, BestFace";
        private string maskImagePath;
        private bool saveFrameImageData = true;
        private int imageQuality;
        private bool mpeG1PreviewEnabled = true;
        private int mpeG1PreviewPort;
        private int mpeG1VideoBitrate;
        private int previewMaxDimension;
        private string serviceName;
        private string[] spoofDetectorResourceIds;
        #endregion

        #region methods
        public string Id
        {
            get => id;
            set => id = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Source
        {
            get => source;
            set => source = value;
        }

        public bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }

        public FaceDetectorConfig FaceDetectorConfig
        {
            get => faceDetectorConfig;
            set => faceDetectorConfig = value;
        }

        public string FaceDetectorResourceId
        {
            get => faceDetectorResourceId;
            set => faceDetectorResourceId = value;
        }

        public PedestrianDetectorConfig PedestrianDetectorConfig
        {
            get => pedestrianDetectorConfig;
            set => pedestrianDetectorConfig = value;
        }

        public string PedestrianDetectorResourceId
        {
            get => pedestrianDetectorResourceId;
            set => pedestrianDetectorResourceId = value;
        }

        public string TemplateGeneratorResourceId
        {
            get => templateGeneratorResourceId;
            set => templateGeneratorResourceId = value;
        }

        public int RedetectionTime
        {
            get => redetectionTime;
            set => redetectionTime = value;
        }

        public int TemplateGenerationTime
        {
            get => templateGenerationTime;
            set => templateGenerationTime = value;
        }

        public string TrackMotionOptimization
        {
            get => trackMotionOptimization;
            set => trackMotionOptimization = value;
        }

        public string FaceSaveStrategy
        {
            get => faceSaveStrategy;
            set => faceSaveStrategy = value;
        }

        public string MaskImagePath
        {
            get => maskImagePath;
            set => maskImagePath = value;
        }

        public bool SaveFrameImageData
        {
            get => saveFrameImageData;
            set => saveFrameImageData = value;
        }

        public int ImageQuality
        {
            get => imageQuality;
            set => imageQuality = value;
        }

        public bool MpeG1PreviewEnabled
        {
            get => mpeG1PreviewEnabled;
            set => mpeG1PreviewEnabled = value;
        }

        public int MpeG1PreviewPort
        {
            get => mpeG1PreviewPort;
            set => mpeG1PreviewPort = value;
        }

        public int MpeG1VideoBitrate
        {
            get => mpeG1VideoBitrate;
            set => mpeG1VideoBitrate = value;
        }

        public int PreviewMaxDimension
        {
            get => previewMaxDimension;
            set => previewMaxDimension = value;
        }

        public string ServiceName
        {
            get => serviceName;
            set => serviceName = value;
        }

        public string[] SpoofDetectorResourceIds
        {
            get => spoofDetectorResourceIds;
            set => spoofDetectorResourceIds = value;
        }

        public SpoofDetectorConfig SpoofDetectorConfig
        {
            get => spoofDetectorConfig;
            set => spoofDetectorConfig = value;
        }

        #endregion
    }
}