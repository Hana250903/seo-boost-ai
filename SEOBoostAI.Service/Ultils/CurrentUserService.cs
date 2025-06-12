using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Ultils
{
    public interface ICurrentUserService
    {
        int GetUserId();
        String getUserEmail();
        Task<User> GetCurrentAccountAsync();
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserRepository _userRepository;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            try
            {
                return int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("user_ID")?.Value);
            }
            catch
            {
                throw new Exception("Login Before USE!!!!");
            }
        }
        public String getUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        public async Task<User?> GetCurrentAccountAsync()
        {
            int userId = GetUserId();
            var account = await _userRepository.GetByIdAsync(userId);
            return account;

            //var user = _httpContextAccessor.HttpContext.User.Identity;
            //if(user == null)
            //{
            //    throw new Exception("Account is not found in the database.");
            //}
            //return (User)user;
        }

    }
}
