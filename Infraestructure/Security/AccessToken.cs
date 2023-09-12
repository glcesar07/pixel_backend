using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infraestructure.Security
{
    public class AccessToken : ITokenService
    {
        private readonly IConfiguration _configuration;

        public AccessToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(dynamic userObject)
        {
            JObject newUserData = new JObject();
            newUserData.Add("username", userObject.username);
            newUserData.Add("role", userObject.roles);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, (string)userObject.username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())                
            };

            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims,
                    expires: null,
                    signingCredentials: credentials
                );

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodeToken;
        }
    }
}
