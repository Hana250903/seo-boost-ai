using Microsoft.EntityFrameworkCore;
using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository() { }

        public async Task<PaginationResult<List<User>>> GetUserWithPaginateAsync(int currentPage, int pageSize)
        {
            var users = await GetAllAsync();

            var totalItems = users.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            users = users
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PaginationResult<List<User>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = users
            };
            return result;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
