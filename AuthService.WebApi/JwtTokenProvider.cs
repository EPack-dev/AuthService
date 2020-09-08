using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Model;
using AuthService.WebApi.Config;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.WebApi
{
    public class JwtTokenProvider : ITokenProvider
    {
        public JwtTokenProvider(AuthConfig authConfig)
        {
            _authConfig = authConfig;
        }

        public string GenerateToken(string login, UserRole role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_authConfig.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_authConfig.LifetimeDays),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private readonly AuthConfig _authConfig;
    }
}
