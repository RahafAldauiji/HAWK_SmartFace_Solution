namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>Camera</c> provide all the attributes
    /// that needed in the camera entity.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Property <c>id</c> represents the camera id.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Property <c>name</c> represents the camera name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Property <c>source</c> represents the
        /// Real Time Streaming Protocol (RTSP) of the camera.
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// Property <c>enabled</c> represents if the
        /// camera is enabled or not 
        /// </summary>
        public bool enabled { get; set; }

        /// <summary>
        /// Property <c>faceDetectorResourceId</c> represents the
        /// face Detector Resource Id.
        /// </summary>
        public string faceDetectorResourceId { get; set; }

        /// <summary>
        /// Property <c>pedestrianDetectorResourceId</c> represents the
        /// pedestrian Detector Resource Id.
        /// </summary>
        public string pedestrianDetectorResourceId { get; set; }

        /// <summary>
        /// Property <c>templateGeneratorResourceId</c> represents the
        /// template Generator Resource Id.
        /// </summary>
        public string templateGeneratorResourceId { get; set; }

        /// <summary>
        /// Property <c>redetectionTime</c> represents the
        /// redetection time of the camera.
        /// </summary>
        public int redetectionTime { get; set; }

        /// <summary>
        /// Property <c>templateGenerationTime</c> represents the
        /// template generation time.
        /// </summary>
        public int templateGenerationTime { get; set; }

        /// <summary>
        /// Property <c>trackMotionOptimization</c> represents the
        /// track motion optimization.
        /// </summary>
        public string trackMotionOptimization { get; set; }

        /// <summary>
        /// Property <c>faceSaveStrategy</c> represents the face save strategy.
        /// <list type="item">
        /// <listheader>
        ///<term>Strategies</term>
        ///<description>Four strategies for saving the face</description>
        /// </listheader>
        /// <item><term>All</term></item>
        /// <item><term>FirstFace</term></item>
        /// <item><term>BestFace</term></item>
        /// <item><term>MatchedOnly</term></item>
        /// </list>
        /// </summary>
        public string faceSaveStrategy { get; set; }

        /// <summary>
        /// Property <c>maskImagePath</c> represents the
        /// mask image path.
        /// </summary>
        public string maskImagePath { get; set; }

        /// <summary>
        /// Property <c>saveFrameImageData</c> represents if
        /// the frame image data should be saved from the camera.
        /// </summary>
        public bool saveFrameImageData { get; set; }

        /// <summary>
        /// Property <c>imageQuality</c> represents 
        /// the quality of the image in pernetage  
        /// </summary>
        public int imageQuality { get; set; }

        public bool mpeG1PreviewEnabled { get; set; }

        public int mpeG1PreviewPort { get; set; }

        public int mpeG1VideoBitrate { get; set; }

        public int previewMaxDimension { get; set; }

        public FaceDetectorConfig faceDetectorConfig { get; set; }

        /// <summary>
        /// Property <c>serviceName</c> represents 
        /// the service name that will run on it   
        /// </summary>
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