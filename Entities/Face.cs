using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>Face</c> is one of the main classes in our system, it will provide all the attribute that needed in Face entity
    /// The Face information is stored in the database and will be fetched as json 
    /// </summary>
    public class Face
    { 
        public string trackletId { get; set; }
        public string faceMaskStatus { get; set; }
        public int quality { get; set; }
        public int templateQuality { get; set; }
        public string imageDataId { get; set; }
        public string processedAt { get; set; }
        public double cropLeftTopX { get; set; }
        public double cropLeftTopY { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public double cropRightTopX { get; set; }
        public double cropRightTopY { get; set; }
        public double cropLeftBottomX { get; set; }
        public double cropLeftBottomY { get; set; }
        public double cropRightBottomX { get; set; }
        public double cropRightBottomY { get; set; }
        public double leftEyeX { get; set; }
        public double leftEyeY { get; set; }
        public double rightEyeX { get; set; }
        public double rightEyeY { get; set; }
        public string frameId { get; set; }
        public double age { get; set; }
        public double gender { get; set; }
        public double faceMaskConfidence { get; set; }
        public double noseTipConfidence { get; set; }
        public string faceArea { get; set; }
        public string faceOrder { get; set; }
        public string facesOnFrameCount { get; set; }
        public double faceAreaChange { get; set; }
        public double yawAngle { get; set; }
        public double pitchAngle { get; set; }
        public double rollAngle { get; set; }
        public string autolearnClusterType { get; set; }
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}