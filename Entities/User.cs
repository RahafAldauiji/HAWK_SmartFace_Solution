using System.Text.Json.Serialization;

namespace SmartfaceSolution.Entities
{
    /// <summary>
    /// Entity <c>User</c> represent the user data in our system
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        [JsonIgnore] // prevent the attribute to be serialized and returned in the response
        public string Password { get; set; }
    }
}