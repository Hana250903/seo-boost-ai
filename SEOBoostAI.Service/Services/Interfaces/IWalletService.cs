using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IWalletService
    {
        Task<PaginationResult<List<Wallet>>> GetWallet(int pageIndex, int pageSize, string? sortOrder);
        
        Task<Wallet> GetWalletByIdAsync(int id);
        
        Task<int> CreateWalletAsync(Wallet wallet);
        
        Task<int> UpdateWalletAsync(Wallet wallet);
        
        Task<bool> DeleteWalletAsync(int id);
        
        Task<Wallet> GetUserWalletsAsync(int userId);
    }
}
