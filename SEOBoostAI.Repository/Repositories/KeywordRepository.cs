using Microsoft.EntityFrameworkCore;
using SEOBoostAI.Repository.GenericRepository;
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
        
        public async Task<List<Keyword>> GetAllAsync(string keyword)
        {
            var list = await _context.Keywords.Where(k => k.Keyword1.Contains(keyword)).ToListAsync();

            return list ?? new List<Keyword>();
        }
    }
}
