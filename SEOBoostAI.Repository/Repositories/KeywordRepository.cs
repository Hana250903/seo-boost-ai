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
    public class KeywordRepository: GenericRepository<Keyword>
    {
        /// <summary>
/// Initializes a new instance of the <see cref="KeywordRepository"/> class with a default <see cref="SEOBoostAIContext"/> if none exists.
/// </summary>
public KeywordRepository() => _context ??= new SEOBoostAIContext();
        /// <summary>
/// Initializes a new instance of the <see cref="KeywordRepository"/> class using the specified database context.
/// </summary>
/// <param name="context">The database context to be used for keyword data operations.</param>
public KeywordRepository(SEOBoostAIContext context) => _context = context;

        /// <summary>
        /// Asynchronously retrieves a paginated, filtered, and sorted list of keywords based on the criteria specified in the provided search request.
        /// </summary>
        /// <param name="keywordSearchRequest">The search and pagination criteria, including filters, sorting options, and page information.</param>
        /// <returns>A <see cref="PaginationResult{List{Keyword}}"/> containing the list of matching keywords and pagination metadata.</returns>
        public async Task<PaginationResult<List<Keyword>>> GetAllAsync(KeywordSearchRequest keywordSearchRequest)
        {
            // Bắt đầu truy vấn
            var query = _context.Keywords.AsQueryable();

            // Áp dụng bộ lọc
            if (!string.IsNullOrEmpty(keywordSearchRequest.Keyword))
            {
                query = query.Where(k => k.Keyword1.Contains(keywordSearchRequest.Keyword));
            }

            if (!string.IsNullOrEmpty(keywordSearchRequest.Competition))
            {
                query = query.Where(k => k.Competition.Equals(keywordSearchRequest.Competition) || k.Competition == null);
            }

            if (!string.IsNullOrEmpty(keywordSearchRequest.Intent))
            {
                query = query.Where(k => k.Intent.Contains(keywordSearchRequest.Intent));
            }
            if (!string.IsNullOrEmpty(keywordSearchRequest.Trend))
            {
                query = query.Where(k => k.Trend.Contains(keywordSearchRequest.Trend));
            }


            // **************** Áp dụng sắp xếp ****************
            if (!string.IsNullOrEmpty(keywordSearchRequest.SortBy))
            {
                switch (keywordSearchRequest.SortBy.ToLower())
                {
                    case "keyword":
                        query = keywordSearchRequest.SortOrder.ToLower() == "desc" ?
                                query.OrderByDescending(k => k.Keyword1) :
                                query.OrderBy(k => k.Keyword1);
                        break;
                    case "difficulty":
                        // Khi sắp xếp các trường có thể null, bạn có thể muốn xử lý đặc biệt (ví dụ: null ở cuối cùng)
                        // Tuy nhiên, đối với số, mặc định null sẽ được xếp ở đầu hoặc cuối tùy thuộc vào hướng sắp xếp.
                        query = keywordSearchRequest.SortOrder.ToLower() == "desc" ?
                                query.OrderByDescending(k => k.Difficulty) :
                                query.OrderBy(k => k.Difficulty);
                        break;
                    case "cpc":
                        query = keywordSearchRequest.SortOrder.ToLower() == "desc" ?
                                query.OrderByDescending(k => k.Cpc) :
                                query.OrderBy(k => k.Cpc);
                        break;
                    case "searchvolume": // Thêm trường SearchVolume nếu bạn muốn sắp xếp theo nó
                        query = keywordSearchRequest.SortOrder.ToLower() == "desc" ?
                                query.OrderByDescending(k => k.SearchVolume) :
                                query.OrderBy(k => k.SearchVolume);
                        break;
                    // Thêm các case khác cho các trường bạn muốn sắp xếp (ví dụ: Competition, Intent, Trend)
                    // Ví dụ:
                    case "competition":
                        query = keywordSearchRequest.SortOrder.ToLower() == "desc" ?
                                query.OrderByDescending(k => k.Competition) :
                                query.OrderBy(k => k.Competition);
                        break;
                    default:
                        // Mặc định sắp xếp theo Keyword1 nếu không có trường sắp xếp hợp lệ
                        query = query.OrderBy(k => k.Keyword1);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(k => k.Keyword1);
            }

            var totalItems = await query.CountAsync();

            var list = await query.Skip((keywordSearchRequest.PageIndex - 1) * keywordSearchRequest.PageSize)
                                  .Take(keywordSearchRequest.PageSize)
                                  .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / keywordSearchRequest.PageSize);

            return new PaginationResult<List<Keyword>>
            {
                Items = list,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = keywordSearchRequest.PageIndex,
                PageSize = keywordSearchRequest.PageSize
            };
        }
    }
}
