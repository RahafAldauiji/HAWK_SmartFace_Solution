using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using SmartfaceSolution.Entities;
using SmartfaceSolution.Helpers;
using SmartfaceSolution.Models;

namespace SmartfaceSolution.Services
{
    /// <summary>
    /// <c>IUserService</c> it is an interface class that specify the user service
    /// and contain the methods that will be used to Authenticate the user and retrieves the user id
    /// </summary>
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        User GetById(int id);
    }

    /// <summary>
    /// <c>UserService</c> is a concrete class that implement IUserService interface 
    /// </summary>
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User {Id = 1, FirstName = "Test", LastName = "User", Username = "Admin", Password = "Admin"}
        };

        private readonly JwtConfig _jwtConfig;

        public UserService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found else generate jwt token and return Authenticate Response
            return user == null ? null : new AuthenticateResponse(user, generateJwtToken(user));
            
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
        

        private string generateJwtToken(User user)
        {
            // JwtSecurityTokenHandler is used to generate the token using the secret key that stored in appsettings.json
            var handler = new JwtSecurityTokenHandler();var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                // The token is valid for just 1 day
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}