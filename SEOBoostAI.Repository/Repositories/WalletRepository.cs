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
    public class WalletRepository: GenericRepository<Wallet>
    {
        public WalletRepository() => _context = new SEOBoostAIContext();
        public WalletRepository(SEOBoostAIContext context) => _context = context;

        public async Task<PaginationResult<List<Wallet>>> GetWallet(int pageIndex, int pageSize, string? sortOrder)
        {
            var query = _context.Wallets.AsQueryable();

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder.ToLower())
                {
                    case "asc":
                        query = query.OrderBy(k => k.Currency);
                        break;
                    case "desc":
                        // Khi sắp xếp các trường có thể null, bạn có thể muốn xử lý đặc biệt (ví dụ: null ở cuối cùng)
                        // Tuy nhiên, đối với số, mặc định null sẽ được xếp ở đầu hoặc cuối tùy thuộc vào hướng sắp xếp.
                        query = query.OrderByDescending(k => k.Currency);
                        break;
                    default:
                        query = query.OrderBy(k => k.CreatedAt);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(k => k.CreatedAt);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PaginationResult<List<Wallet>>
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return result ?? new PaginationResult<List<Wallet>>();
        }

        public async Task<Wallet> GetWalletByUserId(int userId)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}
