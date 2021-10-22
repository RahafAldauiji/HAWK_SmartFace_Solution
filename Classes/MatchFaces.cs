namespace SmartfaceSolution.Classes
{
    public class MatchFaces
    {
        #region variables
        private double cropLeftTopX;
        private double cropLeftTopY;
        private double cropRightTopX;
        private double cropRightTopY;
        private double cropLeftBottomX;
        private double cropLeftBottomY;
        private double cropRightBottomX;
        private double cropRightBottomY;
        private int quality;
        private double leftEyeX;
        private double leftEyeY;
        private double rightEyeX;
        private double rightEyeY;
        private string frameId;
        private double age;

        enum gender
        {
            Female,
            Male
        }
        private double faceMaskConfidence;
        private double noseTipConfidence;
        enum faceMaskStatus
        {
            Unknown,
            Mask,
            NoMask
        };
        private int score;
        private string watchlistMemberId;
        private string displayName;
        private string fullName;
        private string watchlistFullName; 
        private string watchlistDisplayName;
        private string watchlistId;
        
        public double CropLeftTopX
        {
            get => cropLeftTopX;
            set => cropLeftTopX = value;
        }
        
        public double CropLeftTopY
        {
            get => cropLeftTopY;
            set => cropLeftTopY = value;
        }
        
        public double CropRightTopX
        {
            get => cropRightTopX;
            set => cropRightTopX = value;
        }
        
        public double CropRightTopY
        {
            get => cropRightTopY;
            set => cropRightTopY = value;
        }
        
        public double CropLeftBottomX
        {
            get => cropLeftBottomX;
            set => cropLeftBottomX = value;
        }
        
        public double CropLeftBottomY
        {
            get => cropLeftBottomY;
            set => cropLeftBottomY = value;
        }
        
        public double CropRightBottomX
        {
            get => cropRightBottomX;
            set => cropRightBottomX = value;
        }
        
        public double CropRightBottomY
        {
            get => cropRightBottomY;
            set => cropRightBottomY = value;
        }
        
        public int Quality
        {
            get => quality;
            set => quality = value;
        }
        
        public double LeftEyeX
        {
            get => leftEyeX;
            set => leftEyeX = value;
        }
        
        public double LeftEyeY
        {
            get => leftEyeY;
            set => leftEyeY = value;
        }
        
        public double RightEyeX
        {
            get => rightEyeX;
            set => rightEyeX = value;
        }
        
        public double RightEyeY
        {
            get => rightEyeY;
            set => rightEyeY = value;
        }
        
        public string FrameId
        {
            get => frameId;
            set => frameId = value;
        }
        
        public double Age
        {
            get => age;
            set => age = value;
        }
        
        
        
        public double FaceMaskConfidence
        {
            get => faceMaskConfidence;
            set => faceMaskConfidence = value;
        }
        
        public double NoseTipConfidence
        {
            get => noseTipConfidence;
            set => noseTipConfidence = value;
        }

        public int Score
        {
            get => score;
            set => score = value;
        }

        public string WatchlistMemberId
        {
            get => watchlistMemberId;
            set => watchlistMemberId = value;
        }

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string FullName
        {
            get => fullName;
            set => fullName = value;
        }

        public string WatchlistFullName
        {
            get => watchlistFullName;
            set => watchlistFullName = value;
        }

        public string WatchlistDisplayName
        {
            get => watchlistDisplayName;
            set => watchlistDisplayName = value;
        }

        public string WatchlistId
        {
            get => watchlistId;
            set => watchlistId = value;
        }

        // enum type
        // {
        //     Match,
        //     NoMatch
        // };
        // private string watchlistMemberId;
        // private string streamId;
        // private int score;
        // private string createdAt;
        // private string updatedAt;
        // private string trackletId;
        // private string watchlistMemberFullName;
        // private string watchlistMemberDisplayName;
        // private string watchlistFullName;
        // private string watchlistDisplayName;
        // private string watchlistId;
        // private string faceId;
        // private string frameId;
        // private double faceArea;
        // private int faceOrder;
        // private int facesOnFrameCount;
        // private double faceAreaChange;
        // private double faceMaskConfidence;
        // private double noseTipConfidence;
        // enum faceMaskStatus
        // {
        //     Unknown,
        //     Mask,
        //     NoMask
        // };
        // private double yawAngle;
        // private double pitchAngle;
        // private double rollAngle;
        // private int templateQuality;
        // private string id;
        #endregion

        #region methods
        //
        // public string WatchlistMemberId
        // {
        //     get => watchlistMemberId;
        //     set => watchlistMemberId = value;
        // }
        //
        // public string StreamId
        // {
        //     get => streamId;
        //     set => streamId = value;
        // }
        //
        // public int Score
        // {
        //     get => score;
        //     set => score = value;
        // }
        //
        // public string CreatedAt
        // {
        //     get => createdAt;
        //     set => createdAt = value;
        // }
        //
        // public string UpdatedAt
        // {
        //     get => updatedAt;
        //     set => updatedAt = value;
        // }
        //
        // public string TrackletId
        // {
        //     get => trackletId;
        //     set => trackletId = value;
        // }
        //
        // public string WatchlistMemberFullName
        // {
        //     get => watchlistMemberFullName;
        //     set => watchlistMemberFullName = value;
        // }
        //
        // public string WatchlistMemberDisplayName
        // {
        //     get => watchlistMemberDisplayName;
        //     set => watchlistMemberDisplayName = value;
        // }
        //
        // public string WatchlistFullName
        // {
        //     get => watchlistFullName;
        //     set => watchlistFullName = value;
        // }
        //
        // public string WatchlistDisplayName
        // {
        //     get => watchlistDisplayName;
        //     set => watchlistDisplayName = value;
        // }
        //
        // public string WatchlistId
        // {
        //     get => watchlistId;
        //     set => watchlistId = value;
        // }
        //
        // public string FaceId
        // {
        //     get => faceId;
        //     set => faceId = value;
        // }
        //
        // public string FrameId
        // {
        //     get => frameId;
        //     set => frameId = value;
        // }
        //
        // public double FaceArea
        // {
        //     get => faceArea;
        //     set => faceArea = value;
        // }
        //
        // public int FaceOrder
        // {
        //     get => faceOrder;
        //     set => faceOrder = value;
        // }
        //
        // public int FacesOnFrameCount
        // {
        //     get => facesOnFrameCount;
        //     set => facesOnFrameCount = value;
        // }
        //
        // public double FaceAreaChange
        // {
        //     get => faceAreaChange;
        //     set => faceAreaChange = value;
        // }
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
        //
        // public double YawAngle
        // {
        //     get => yawAngle;
        //     set => yawAngle = value;
        // }
        //
        // public double PitchAngle
        // {
        //     get => pitchAngle;
        //     set => pitchAngle = value;
        // }
        //
        // public double RollAngle
        // {
        //     get => rollAngle;
        //     set => rollAngle = value;
        // }
        //
        // public int TemplateQuality
        // {
        //     get => templateQuality;
        //     set => templateQuality = value;
        // }
        //
        // public string Id
        // {
        //     get => id;
        //     set => id = value;
        // }
        #endregion
    }
}