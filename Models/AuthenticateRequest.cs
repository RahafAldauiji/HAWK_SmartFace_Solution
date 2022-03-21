using System.ComponentModel.DataAnnotations;

namespace SmartfaceSolution.Models
{
    /// <summary>
    /// <c>AuthenticateRequest</c> handle authentication requests
    /// DataAnnotations is automatically validate the user
    /// where [Required] are set on the attribute that must be validated
    /// if one of the attribute missing the validation will failed and error message will returned 
    /// </summary>
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}