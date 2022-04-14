using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

        User GetById(int id);
    }

    /// <summary>
    /// <c>UserService</c> is a concrete class that implement IUserService interface 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ServerConfig _serverName;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications


        private readonly JwtConfig _jwtConfig;
        private User _user;

        public UserService(IOptions<JwtConfig> jwtConfig, IOptions<ServerConfig> serverConfig)
        {
            _jwtConfig = jwtConfig.Value;
            _serverName = serverConfig.Value;
        }


        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            SqlConnection cnn = null;
            string sqlCommand;
            SqlCommand cmd;
            SqlDataReader dr = null;
            try
            {
                cnn = new SqlConnection(_serverName.DefaultConnection);
                cnn.Open();
                sqlCommand = "SELECT * FROM [dbo].[Employees] WHERE UserName = @UserName AND Password=@Password"; // sql select command to be executed
                cmd = new SqlCommand(sqlCommand, cnn);
                cmd.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, -1).Value = model.Username;
                cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, -1).Value = model.Password;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _user = new User
                        {
                            Id = (int) (dr["Id"]),
                            Username = (string) dr["UserName"],
                            Password = (string) dr["Password"]
                        };
                    }
                }
            }
            finally
            {
                cnn.Close();
            }
            // return null if user not found else generate jwt token and return Authenticate Response
            return _user == null ? null : new AuthenticateResponse(_user, generateJwtToken(_user));
        }

        public User GetById(int id)
        {
            SqlConnection cnn = null;
            string sqlCommand;
            SqlCommand cmd;
            SqlDataReader dr = null;
            try
            {
                cnn = new SqlConnection(_serverName.DefaultConnection);
                cnn.Open();
                sqlCommand =
                    "SELECT * FROM [dbo].[Employees] WHERE Id = @id"; // sql select command to be executed
                cmd = new SqlCommand(sqlCommand, cnn);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return new User
                        {
                            Id = (int) (dr["Id"]),
                            Username = (string) dr["UserName"],
                            Password = (string) dr["Password"]
                        };
                    }
                }
            }
            finally
            {
                cnn.Close();
            }

            return null;
        }


        private string generateJwtToken(User user)
        {
            // JwtSecurityTokenHandler is used to generate the token using the secret key that stored in appsettings.json
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
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