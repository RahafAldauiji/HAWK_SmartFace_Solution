using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartfaceSolution.Helpers;
using SmartfaceSolution.Services;

namespace SmartfaceSolution.Middleware
{
    /// <summary>
    /// <c>JwtMiddleware</c>checks if the request have token in the header
    /// if the token is exists
    /// <list type="bullet">
    /// <item>
    /// <description>It will validate the jwt token</description>
    /// </item>
    /// <item>
    /// <description>get the user id</description>
    /// </item>
    /// <item>
    /// <description>Attach the user to the HttpContext</description>
    /// </item>
    /// </list>
    /// the user will be only attached to the  HttpContext 
    /// if the user authenticated and have valid JWT token
    /// otherwise the authorization will be failed 
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _jwtConfig;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtConfig> jwtConfig)
        {
            _next = next;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            // getting the token from the header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
            if (token != null) // if there is token in the header
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                // getting the secret key from the JwtConfig class
                var jwtKey = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
                // Checking if the token is valid if the validation failed it will throw exception 
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                // if the has been validated the user will be attach to context 
                context.Items["User"] = userService.GetById(userId);
            }
            catch
            {
                // if the validation failed do nothing
            }
        }
    }
}