using SEOBoostAI.Repository.ModelExtensions;
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
    public class WalletService: IWalletService
    {
        private readonly WalletRepository _walletRepository;

        public WalletService(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<int> CreateWalletAsync(Wallet wallet)
        {
            return await _walletRepository.CreateAsync(wallet);
        }

        public async Task<bool> DeleteWalletAsync(int id)
        {
            var wallet = await _walletRepository.GetByIdAsync(id);
            return await _walletRepository.RemoveAsync(wallet);
        }

        public async Task<Wallet> GetUserWalletsAsync(int userId)
        {
            return await _walletRepository.GetWalletByUserId(userId);
        }

        public async Task<PaginationResult<List<Wallet>>> GetWallet(int pageIndex, int pageSize, string? sortOrder)
        {
            return await _walletRepository.GetWallet(pageIndex, pageSize, sortOrder);
        }

        public async Task<Wallet> GetWalletByIdAsync(int id)
        {
            return await _walletRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateWalletAsync(Wallet wallet)
        {
            return await _walletRepository.UpdateAsync(wallet);
        }
    }
}
