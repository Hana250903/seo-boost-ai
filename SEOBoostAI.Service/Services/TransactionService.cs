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
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<int> AddAsync(Transaction transaction)
        {
            return await _transactionRepository.CreateAsync(transaction);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return await _transactionRepository.RemoveAsync(transaction);
        }

        public async Task<PaginationResult<List<Transaction>>> GetAllAsync(int? userId, string? status, string? type, int pageIndex, int pageSize)
        {
            return await _transactionRepository.GetTransactionsByUserId(userId, status, type, pageIndex, pageSize);
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Transaction transaction)
        {
            return await _transactionRepository.UpdateAsync(transaction);
        }

        public async Task<int> UpdateTransactionStatus(int transactionId, string status, string type)
        {
            return await _transactionRepository.UpdateTransactionStatus(transactionId, status, type);
        }
    }
}
