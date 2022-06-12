namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>MemberMatch</c> provide all the attribute that needed in Member Match entity
    /// </summary>
    public class MemberMatch
    {
        public string CropImage { get; set; }
        public long FrameTimestampMicroseconds{ get; set; }
        public double LeftEyeX{ get; set; }
        public double LeftEyeY{ get; set; }
        public double RightEyeX{ get; set; }
        public double RightEyeY{ get; set; }
        public string Id{ get; set; }
        public string Type{ get; set; }
        public string WatchlistMemberId{ get; set; }
        public string StreamId{ get; set; }
        public int Score{ get; set; }
        public string FrameId{ get; set; }
        public double FaceArea{ get; set; }
        public int FaceOrder{ get; set; }
        public int FacesOnFrameCount{ get; set; }
        public double FaceAreaChange{ get; set; }
        public double FaceMaskConfidence{ get; set; }
        public double NoseTipConfidence{ get; set; }
        public string FaceMaskStatus{ get; set; }
        public string CreatedAt{ get; set; }
        public string UpdatedAt { get; set; }
        public string TrackletId{ get; set; }
        public string WatchlistMemberFullName{ get; set; }
        public string WatchlistMemberDisplayName{ get; set; }
        public string WatchlistFullName{ get; set; }
        public string WatchlistDisplayName{ get; set; }
        public string WatchlistId{ get; set; }
        public int TemplateQuality{ get; set; }
        public double YawAngle{ get; set; }
        public double PitchAngle{ get; set; }
        public double RollAngle{ get; set; }
    }

   
}