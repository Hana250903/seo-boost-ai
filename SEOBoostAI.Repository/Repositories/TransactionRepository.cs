using Microsoft.EntityFrameworkCore;
using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {
        public TransactionRepository() => _context = new SEOBoostAIContext();
        public TransactionRepository(SEOBoostAIContext context) => _context = context;

        public async Task<PaginationResult<List<Transaction>>> GetTransactionsByUserId(int? userId, string? status, string? type, int pageIndex, int pageSize)
        {
            var term = await _context.Transactions.Include(t => t.Wallet).Where(u => (u.Wallet.UserId == userId || userId == 0 || userId == null) && (u.Status.Contains(status) || status == null)
                    && (u.Type.Contains(type) || type == null))
                .ToListAsync();
            
            var totalItems = term.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = term.OrderByDescending(t => t.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PaginationResult<List<Transaction>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Items = items
            };

            return result ?? new PaginationResult<List<Transaction>>();
        }

        public async Task<int> UpdateTransactionStatus(int transactionId, string status, string type)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction == null)
            {
                return 0; // Transaction not found
            }

            var wallet = await _context.Wallets.FindAsync(transaction.WalletId);
            if (wallet == null)
            {
                return 0; // Wallet not found
            }

            if (status.Contains("Completed")&& type.Contains("Deposit"))
            {
                wallet.Currency += transaction.Money;
                _context.Wallets.Update(wallet);
            }

            if (status.Contains("Completed")&& type.Contains("Purchase"))
            {
                if (wallet.Currency < transaction.Money)
                {
                    return 0; // Insufficient funds
                }

                wallet.Currency -= transaction.Money;
                _context.Wallets.Update(wallet);
            }

            transaction.Status = status;
            _context.Transactions.Update(transaction);
            return await _context.SaveChangesAsync();
        }
    }
}
