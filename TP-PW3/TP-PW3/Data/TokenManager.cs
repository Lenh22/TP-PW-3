using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TP_PW3.Models;

namespace TP_PW3.Data
{
    public class TokenManager
    {
        private static string Secret = "4c2adnvdff2610443a5477834ce698b5ee643b84274e751612940d641401fbc7"; //Llave secreta 5:14 capaz la clave esta mal


        public static TokenClass GenerateToken(string username)
        {
            TokenClass TokenModel = new TokenClass();   
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name,username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            TokenModel.TokenOrMessage = handler.WriteToken(token);
            TokenModel.Expired = token.ValidTo;
            return TokenModel;
        }

        public static ClaimsPrincipal GetPrincipal(string token) {
            try {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)handler.ReadToken(token);
                if (jwtToken == null) return null;

                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = handler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch(Exception) {
                return null;
            }

        }

        public static string ValidateToken(string token) {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if(principal == null) return null;

            ClaimsIdentity identity = null;

            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException) {
                return null;
            }
            Claim claim = identity.FindFirst(ClaimTypes.Name);
            username = claim.Value;
            return username;
        }
    }
}
