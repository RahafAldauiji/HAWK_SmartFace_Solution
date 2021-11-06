namespace SmartfaceSolution.Classes
{
    public class SpoofDetectorConfig
    {
     
        // private double externalScoreThreshold;
        // private double distantLivenessScoreThreshold;
        // private double nearbyLivenessScoreThreshold;
        // private string distantLivenessConditions;
        // private string nearbyLivenessConditions;

   
        public double externalScoreThreshold { get; set; }

        public double distantLivenessScoreThreshold { get; set; }

        public double nearbyLivenessScoreThreshold { get; set; }

        public string distantLivenessConditions { get; set; }

        public string nearbyLivenessConditions { get; set; }

    }
}