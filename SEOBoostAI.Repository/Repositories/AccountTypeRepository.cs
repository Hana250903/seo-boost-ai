using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.Repositories
{
    public class AccountTypeRepository: GenericRepository<AccountType>
    {
        public AccountTypeRepository() => _context ??= new SEOBoostAIContext();
        public AccountTypeRepository(SEOBoostAIContext context) => _context = context;
    }
}
