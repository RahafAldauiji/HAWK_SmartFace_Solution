namespace SmartfaceSolution.Classes
{
    public class FaceDetectorConfig
    {
        #region variables

        private double minFaceSize;
        private double maxFaceSize;
        private double maxFaces;
        private double confidenceThreshold;

        #endregion

        #region methods

        public double MinFaceSize
        {
            get => minFaceSize;
            set => minFaceSize = value;
        }

        public double MaxFaceSize
        {
            get => maxFaceSize;
            set => maxFaceSize = value;
        }

        public double MaxFaces
        {
            get => maxFaces;
            set => maxFaces = value;
        }

        public double ConfidenceThreshold
        {
            get => confidenceThreshold;
            set => confidenceThreshold = value;
        }

        #endregion
    }
}