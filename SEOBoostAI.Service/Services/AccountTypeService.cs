using SEOBoostAI.Repository.Models;
using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services
{
    public class AccountTypeService: IAccountTypeService
    {
        private readonly AccountTypeRepository _accountTypeRepository;

        public AccountTypeService(AccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }

        public async Task<int> AddAsync(AccountType accountType)
        {
            return await _accountTypeRepository.CreateAsync(accountType);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingAccountType = await _accountTypeRepository.GetByIdAsync(id);
            return await _accountTypeRepository.RemoveAsync(existingAccountType);
        }

        public async Task<List<AccountType>> GetAllAsync()
        {
            return await _accountTypeRepository.GetAllAsync();
        }

        public async Task<AccountType> GetByIdAsync(int id)
        {
            return await _accountTypeRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(AccountType accountType)
        {
            return await _accountTypeRepository.UpdateAsync(accountType);
        }
    }
}
