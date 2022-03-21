using SmartfaceSolution.Entities;

namespace SmartfaceSolution.Models
{
    /// <summary>
    /// <c>AuthenticateResponse</c> has the data that will be returned to the Authenticated user
    /// </summary>
    public class AuthenticateResponse
    {
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}