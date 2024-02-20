using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinancialManagement.Models;
using Microsoft.IdentityModel.Tokens;

namespace FinancialManagement.Service
{
    public class TokenService
    {
        public object Generate(User user)
        {
            Constants constants = new Constants();
            var key = Encoding.ASCII.GetBytes(constants.KEYTOKEN);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name , user.Username),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                userId = user.Id,
                token = tokenString
            };
        }
    }
}