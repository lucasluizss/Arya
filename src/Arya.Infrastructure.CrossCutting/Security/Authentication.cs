using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Arya.Infrastructure.CrossCutting.Security
{
    public static class Authentication
    {
        private static JwtSecurityTokenHandler TokenHandler => new JwtSecurityTokenHandler();

        public static ClaimsPrincipal GetClaimsPrincipal(string token, string securityKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
            };

            return TokenHandler.ValidateToken(token.Replace("Bearer ", ""), tokenValidationParameters, out _);
        }
    }
}
