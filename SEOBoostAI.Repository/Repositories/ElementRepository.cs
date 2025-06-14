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
    public class ElementRepository : GenericRepository<Element>
    {
        public ElementRepository() { }

        /// <summary>
        /// Retrieves a paginated list of Element entities along with pagination metadata.
        /// </summary>
        /// <param name="currentPage">The current page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A PaginationResult containing the list of Element entities for the specified page and pagination details.</returns>
        public async Task<PaginationResult<List<Element>>> GetUserWithPaginateAsync(int currentPage, int pageSize)
        {
            var elements = await GetAllAsync();

            var totalItems = elements.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            elements = elements
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PaginationResult<List<Element>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = elements
            };
            return result;
        }

        /// <summary>
        /// Adds a list of Element entities to the database in a single batch operation.
        /// </summary>
        /// <param name="elements">The list of Element entities to add.</param>
        /// <returns>The number of rows affected if the operation is successful; otherwise, 0.</returns>
        public async Task<int> CreateRangeAsync(List<Element> elements)
        {
            await _context.Elements.AddRangeAsync(elements);
            var result = await _context.SaveChangesAsync(); // Chỉ 1 lần hit DB
            if (result > 0)
            {
                return result;
            }

            return 0;
        }
    }
}
