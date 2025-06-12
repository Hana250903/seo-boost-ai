using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserRepository _userRepository;

        public AuthenService(IConfiguration configuration, UserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> LoginWithGoogle(string credential)
        {
            string clientId = _configuration["GoogleCredential:ClientId"];

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { clientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

            if (payload == null)
            {
                throw new Exception("Credential không hợp lệ");
            }

            var existUser = await _userRepository.GetUserByEmailAsync(payload.Email);

            //nếu có user
            if (existUser != null)
            {
                var accessToken = await GenerateAccessToken(existUser);
                var refreshToken = GenerateRefreshToken(existUser.Email);

                existUser.RefreshToken = refreshToken;

                await _userRepository.UpdateAsync(existUser);

                return accessToken;
            }
            else
            {
                try
                {
                    User newUser = new User()
                    {
                        Username = payload.Email.Split('@')[0].Trim(),
                        FullName = payload.Name,
                        Email = payload.Email,
                        Role = "User",
                        Avatar = "".Trim(),
                        AccountType = "Free",
                        Password = "".Trim(),
                        CreatedAt = DateTime.UtcNow,
                        GoogleId = payload.JwtId
                    };

                    //newUser.Wallet = new Wallet
                    //{
                    //    Currency = 0.0M
                    //};

                    var accessToken = await GenerateAccessToken(newUser);
                    var refreshToken = GenerateRefreshToken(newUser.Email);

                    newUser.RefreshToken = refreshToken;

                    var result = await _userRepository.CreateAsync(newUser);

                    if (result > 0)
                    {
                        return accessToken;
                    }

                    throw new Exception("");
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<bool> LogOut(string RefreshToken, int userId)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authSigningKey,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                SecurityToken validatedToken;
                var principal = handler.ValidateToken(RefreshToken, validationParameters, out validatedToken);
                var email = principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                if (email != null)
                {
                    var user = await _userRepository.GetByIdAsync(userId);
                    if (email == user.Email)
                    {
                        var existUser = await _userRepository.GetUserByEmailAsync(email);

                        if (existUser != null)
                        {
                            existUser.RefreshToken = null;
                            await _userRepository.UpdateAsync(existUser);

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GenerateAccessToken(User user)
        {
            var authClaims = new List<Claim>();

            authClaims.Add(new Claim("email", user.Email.ToString()));
            authClaims.Add(new Claim("fullname", user.FullName.ToString()));
            authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            authClaims.Add(new Claim("user_ID", user.Id.ToString()));
            authClaims.Add(new Claim("role", user.Role.ToString()));
            var accessToken = GenerateJWTToken.CreateToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateRefreshToken(string email)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("email", email.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var refreshToken = GenerateJWTToken.CreateRefreshToken(claims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }
    }
}
