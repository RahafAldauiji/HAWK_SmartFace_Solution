using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace SmartfaceSolution.Classes
{
    public class MemberFaces
    {
        private List<Face> items;

        public List<Face> Items
        {
            get => items;
            set => items = value;
        }
    }
    public class Face
    {
        #region variables

        private string trackletId;
        private int quality;
        private int templateQuality;

        enum state
        {
            New,
            Extracting,
            Extracted,
            Error
        };

        private string imageDataId;
        private string processedAt;
        private double cropLeftTopX;
        private double cropLeftTopY;
        private double cropRightTopX;
        private double cropRightTopY;
        private double cropLeftBottomX;
        private double cropLeftBottomY;
        private double cropRightBottomX;
        private double cropRightBottomY;
        private double leftEyeX;
        private double leftEyeY;
        private double rightEyeX;
        private double rightEyeY;
        private string frameId;

        enum type
        {
            Regular,
            AutoLearn
        };

        private double age;
        private double gender;
        private double faceMaskConfidence;
        private double noseTipConfidence;

        enum faceMaskStatus
        {
            Unknown,
            Mask,
            NoMask
        };

        private string faceArea;
        private string faceOrder;
        private string facesOnFrameCount;
        private double faceAreaChange;
        private double yawAngle;
        private double pitchAngle;
        private double rollAngle;
        private string autolearnClusterType;
        private string id;
        private string createdAt;
        private string updatedAt;

        #endregion

        #region methods

        public string TrackletId1
        {
            get => trackletId;
            set => trackletId = value;
        }

        public int Quality
        {
            get => quality;
            set => quality = value;
        }

        public int TemplateQuality
        {
            get => templateQuality;
            set => templateQuality = value;
        }


        public string ImageDataId
        {
            get => imageDataId;
            set => imageDataId = value;
        }

        public string ProcessedAt
        {
            get => processedAt;
            set => processedAt = value;
        }

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

        public double Gender
        {
            get => gender;
            set => gender = value;
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


        public string FaceArea
        {
            get => faceArea;
            set => faceArea = value;
        }
        
        public string FaceOrder
        {
            get => faceOrder;
            set => faceOrder = value;
        }
        
        public string FacesOnFrameCount
        {
            get => facesOnFrameCount;
            set => facesOnFrameCount = value;
        }

        public double FaceAreaChange
        {
            get => faceAreaChange;
            set => faceAreaChange = value;
        }

        public double YawAngle
        {
            get => yawAngle;
            set => yawAngle = value;
        }

        public double PitchAngle
        {
            get => pitchAngle;
            set => pitchAngle = value;
        }

        public double RollAngle
        {
            get => rollAngle;
            set => rollAngle = value;
        }

        public string AutolearnClusterType
        {
            get => autolearnClusterType;
            set => autolearnClusterType = value;
        }

        public string Id
        {
            get => id;
            set => id = value;
        }

        public string CreatedAt
        {
            get => createdAt;
            set => createdAt = value;
        }

        public string UpdatedAt
        {
            get => updatedAt;
            set => updatedAt = value;
        }

        #endregion
    }
}