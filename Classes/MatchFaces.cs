namespace SmartfaceSolution.Classes
{
    public class MatchFaces
    {
        #region variables

        // private double cropLeftTopX;
        // private double cropLeftTopY;
        // private double cropRightTopX;
        // private double cropRightTopY;
        // private double cropLeftBottomX;
        // private double cropLeftBottomY;
        // private double cropRightBottomX;
        // private double cropRightBottomY;
        // private int quality;
        // private double leftEyeX;
        // private double leftEyeY;
        // private double rightEyeX;
        // private double rightEyeY;
        // private string frameId;
        // private double age;
        // private string gender;
        // private double faceMaskConfidence;
        // private double noseTipConfidence;
        // private string faceMaskStatus;
        private MatchResult[] matchResults;

        #endregion

        #region methods

        // public double CropLeftTopX
        // {
        //     get => cropLeftTopX;
        //     set => cropLeftTopX = value;
        // }
        //
        // public string Gender
        // {
        //     get => gender;
        //     set => gender = value;
        // }
        //
        // public string FaceMaskStatus
        // {
        //     get => faceMaskStatus;
        //     set => faceMaskStatus = value;
        // }
        //
        // public double CropLeftTopY
        // {
        //     get => cropLeftTopY;
        //     set => cropLeftTopY = value;
        // }
        //
        // public double CropRightTopX
        // {
        //     get => cropRightTopX;
        //     set => cropRightTopX = value;
        // }
        //
        // public double CropRightTopY
        // {
        //     get => cropRightTopY;
        //     set => cropRightTopY = value;
        // }
        //
        // public double CropLeftBottomX
        // {
        //     get => cropLeftBottomX;
        //     set => cropLeftBottomX = value;
        // }
        //
        // public double CropLeftBottomY
        // {
        //     get => cropLeftBottomY;
        //     set => cropLeftBottomY = value;
        // }
        //
        // public double CropRightBottomX
        // {
        //     get => cropRightBottomX;
        //     set => cropRightBottomX = value;
        // }
        //
        // public double CropRightBottomY
        // {
        //     get => cropRightBottomY;
        //     set => cropRightBottomY = value;
        // }
        //
        // public int Quality
        // {
        //     get => quality;
        //     set => quality = value;
        // }
        //
        // public double LeftEyeX
        // {
        //     get => leftEyeX;
        //     set => leftEyeX = value;
        // }
        //
        // public double LeftEyeY
        // {
        //     get => leftEyeY;
        //     set => leftEyeY = value;
        // }
        //
        // public double RightEyeX
        // {
        //     get => rightEyeX;
        //     set => rightEyeX = value;
        // }
        //
        // public double RightEyeY
        // {
        //     get => rightEyeY;
        //     set => rightEyeY = value;
        // }
        //
        // public string FrameId
        // {
        //     get => frameId;
        //     set => frameId = value;
        // }
        //
        // public double Age
        // {
        //     get => age;
        //     set => age = value;
        // }
        //
        //
        // public double FaceMaskConfidence
        // {
        //     get => faceMaskConfidence;
        //     set => faceMaskConfidence = value;
        // }
        //
        // public double NoseTipConfidence
        // {
        //     get => noseTipConfidence;
        //     set => noseTipConfidence = value;
        // }

        public MatchResult[] MatchResults
        {
            get => matchResults;
            set => matchResults = value;
        }

        #endregion
    }
}