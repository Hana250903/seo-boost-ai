using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IElementService
    {
        Task<List<Element>> GetAllElementAsync();
        Task<PaginationResult<List<Element>>> GetElementWithPaginateAsync(int currentPage, int pageSize);
        Task<Element> GetElementByIdAsync(int id);
        Task<int> CreateElementAsync(Element element);
        Task<int> UpdateElementAsync(Element element);
        Task<bool> DeleteElementAsync(int id);
    }
}
