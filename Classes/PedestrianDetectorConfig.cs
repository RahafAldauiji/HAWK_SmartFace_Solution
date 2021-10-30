namespace SmartfaceSolution.Classes
{
    public class PedestrianDetectorConfig
    {
        #region variables

        private double minPedestrianSize;
        private double maxPedestrianSize;
        private double maxPedestrians;
        private double confidenceThreshold;

        #endregion

        #region methods

        public double MinPedestrianSize
        {
            get => minPedestrianSize;
            set => minPedestrianSize = value;
        }

        public double MaxPedestrianSize
        {
            get => maxPedestrianSize;
            set => maxPedestrianSize = value;
        }

        public double MaxPedestrians
        {
            get => maxPedestrians;
            set => maxPedestrians = value;
        }

        public double ConfidenceThreshold
        {
            get => confidenceThreshold;
            set => confidenceThreshold = value;
        }

        #endregion
    }
}