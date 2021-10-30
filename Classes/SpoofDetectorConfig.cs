namespace SmartfaceSolution.Classes
{
    public class SpoofDetectorConfig
    {
        #region variables

        private double externalScoreThreshold;
        private double distantLivenessScoreThreshold;
        private double nearbyLivenessScoreThreshold;
        private string distantLivenessConditions;
        private string nearbyLivenessConditions;

        #endregion

        #region methods

        public double ExternalScoreThreshold
        {
            get => externalScoreThreshold;
            set => externalScoreThreshold = value;
        }

        public double DistantLivenessScoreThreshold
        {
            get => distantLivenessScoreThreshold;
            set => distantLivenessScoreThreshold = value;
        }

        public double NearbyLivenessScoreThreshold
        {
            get => nearbyLivenessScoreThreshold;
            set => nearbyLivenessScoreThreshold = value;
        }

        public string DistantLivenessConditions
        {
            get => distantLivenessConditions;
            set => distantLivenessConditions = value;
        }

        public string NearbyLivenessConditions
        {
            get => nearbyLivenessConditions;
            set => nearbyLivenessConditions = value;
        }

        #endregion
    }
}