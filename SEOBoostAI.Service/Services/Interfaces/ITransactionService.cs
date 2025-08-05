using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<PaginationResult<List<Transaction>>> GetAllAsync(int? userId, string? status, string? type, int pageIndex, int pageSize);
        Task<Transaction> GetByIdAsync(int id);
        Task<int> AddAsync(Transaction transaction);
        Task<int> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(int id);
        Task<int> UpdateTransactionStatus(int transactionId, string status, string type);
    }
}
