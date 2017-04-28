using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JSONFirebirdWebServiceTest.JWT
{
    public class JWTTest
    {
        //This JWT implementation is from: http://stackoverflow.com/questions/40281050/jwt-authentication-for-asp-net-web-api

        public const string Secret = "856FECBA3B06519C8DDDBC80BB080553"; // your symetric
        //testing Git changes
        //reduce expireMinutes to test expired tokens on the client side.
        public string GenerateToken(string username, int expireMinutes = 2)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var domain = "http://localhost";
            var allowedAudience = "http://localhost";

            var now = DateTime.UtcNow;
            var expiry = now.AddSeconds(5);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Country, "Canada"),
                            new Claim(ClaimTypes.Expiration, expiry.Ticks.ToString()),
                            new Claim(ClaimTypes.Webpage, allowedAudience),
                            new Claim(ClaimTypes.Uri, domain),
                            new Claim("CustomSecurity","CustomSecurityValue")
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
