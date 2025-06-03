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
        
    }
}
