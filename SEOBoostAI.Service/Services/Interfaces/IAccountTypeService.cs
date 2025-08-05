using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IAccountTypeService
    {
        Task<List<AccountType>> GetAllAsync();
        Task<AccountType> GetByIdAsync(int id);
        Task<int> AddAsync(AccountType accountType);
        Task<int> UpdateAsync(AccountType accountType);
        Task<bool> DeleteAsync(int id);
    }
}
