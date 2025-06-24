using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services
{
    public class ElementService : IElementService
    {
        private readonly ElementRepository _repository;

        public ElementService() => _repository = new ElementRepository();

        public async Task<List<Element>> AnalyzePageAsync(string url, int auditId)
        {
            return await HtmlElementAnalyzer.AnalyzeUrlAsync(url, auditId);
        }

        public async Task<int> CreateElementAsync(Element element)
        {
            return await _repository.CreateAsync(element);
        }

        public async Task<int> CreateElementsAsync(List<Element> elements)
        {
            return await _repository.CreateRangeAsync(elements);
        }

        public async Task<bool> DeleteElementAsync(int id)
        {
            var element = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(element);
        }

        public async Task<List<Element>> ElementNotPass(int auditId)
        {
            return await _repository.ElementNotPass(auditId);
        }

        public async Task<List<Element>> GetAllElementAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Element> GetElementByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<PaginationResult<List<Element>>> GetElementWithPaginateAsync(int currentPage, int pageSize)
        {
            return await _repository.GetUserWithPaginateAsync(currentPage, pageSize);
        }

        public async Task<int> UpdateElementAsync(Element element)
        {
            return await _repository.UpdateAsync(element);
        }
    }
}
