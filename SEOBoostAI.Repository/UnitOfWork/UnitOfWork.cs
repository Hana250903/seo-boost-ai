using Microsoft.EntityFrameworkCore;
using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.UnitOfWork
{
    public class UnitOfWork<T> where T : class
    {
        private readonly SEOBoostAIContext _context;
        public GenericRepository<T> Repository => new GenericRepository<T>(_context);

        public UnitOfWork(SEOBoostAIContext context)
        {
            _context = context;
        }

        public async Task<PaginationResult<List<T>>> GetAllAsync(int currentPage, int pageSize)
        {
            var term = await _context.Set<T>().ToListAsync();
            var totalItems = term.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var items = term.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationResult<List<T>>()
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = items
            };
        }
    }
}
