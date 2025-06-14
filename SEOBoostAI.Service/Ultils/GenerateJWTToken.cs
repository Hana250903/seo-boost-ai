using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Ultils
{
    public class GenerateJWTToken
    {
        /// <summary>
        /// Generates a JWT access token with the specified claims and expiration, using configuration settings for issuer, audience, secret key, and validity period.
        /// </summary>
        /// <param name="authClaims">The claims to include in the JWT token.</param>
        /// <param name="currentTime">The base time from which the token's expiration is calculated.</param>
        /// <returns>A signed <see cref="JwtSecurityToken"/> representing the access token.</returns>
        public static JwtSecurityToken CreateToken(List<Claim> authClaims, IConfiguration configuration, DateTime currentTime)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            _ = int.TryParse(configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: currentTime.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        /// <summary>
        /// Generates a JWT refresh token with the specified claims and expiration based on configuration settings.
        /// </summary>
        /// <param name="authClaims">Claims to include in the refresh token.</param>
        /// <param name="currentTime">The base time from which the token's expiration is calculated.</param>
        /// <returns>A <see cref="JwtSecurityToken"/> representing the refresh token.</returns>
        public static JwtSecurityToken CreateRefreshToken(List<Claim> authClaims, IConfiguration configuration, DateTime currentTime)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityTime);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: currentTime.AddDays(tokenValidityTime),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
