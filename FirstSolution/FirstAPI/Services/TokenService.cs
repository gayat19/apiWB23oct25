using FirstAPI.Interfaces;
using FirstAPI.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace FirstAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _symKey;

        public TokenService(IConfiguration configuration) 
        {
            string _key = configuration.GetSection("Keys:TokenKey").Value;
            _symKey  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        }
        public string CreateToken(UserLoginResponse user)
        {
            string token = string.Empty;
            var creds = new SigningCredentials(_symKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role?? "Customer")
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            token = tokenHandler.WriteToken(securityToken);
            return token;

        }
    }
}
