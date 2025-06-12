using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUser()
        {
            return await _userService.GetAllUserAsync();
        }

        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<PaginationResult<List<User>>> GetAllPagination(int currentPage, int pageSize)
        {
            return await _userService.GetUserWithPaginateAsync(currentPage, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserById(int id)
        {
            return await _userService.GetUsersByIdAsync(id);
        }

        [HttpPost]
        public async Task<int> CreateUser(User user)
        {
            return await _userService.CreateUserAsync(user);
        }

        [HttpPut]
        public async Task<int> UpdateUser(User user)
        {
            return await _userService.UpdateUserAsync(user);
        }

        [HttpDelete]
        public async Task<bool> DeleteUser(int id)
        {
            return await _userService.DeleteUserAsync(id);
        }
    }
}
