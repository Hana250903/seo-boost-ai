using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.Repositories
{
    public class ContentOptimizationRepository : GenericRepository<ContentOptimization>
    {
        public ContentOptimizationRepository() => _context ??= new SEOBoostAIContext();

        public ContentOptimizationRepository(SEOBoostAIContext context) => _context = context;

        
    }
}
