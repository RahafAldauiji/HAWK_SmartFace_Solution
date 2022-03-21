using System.Collections.Generic;

namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>MemberFaces</c> has the member faces that are stored in the database
    /// </summary>
    public class MemberFaces
    {
        public List<Face> items { get; set; }
    }
}