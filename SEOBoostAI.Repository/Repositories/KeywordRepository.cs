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
    public class KeywordRepository: GenericRepository<Keyword>
    {
        public KeywordRepository() => _context ??= new SEOBoostAIContext();
        public KeywordRepository(SEOBoostAIContext context) => _context = context;
        
        public async Task<PaginationResult<List<Keyword>>> GetAllAsync(string keyword, int pageIndex, int pageSize)
        {
            var list = await _context.Keywords.Where(k => k.Keyword1.Contains(keyword))
                .ToListAsync();

            var totalItems = list.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            list = list.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResult<List<Keyword>>
            {
                Items = list,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageIndex,
                PageSize = pageSize
            };
        }
    }
}
