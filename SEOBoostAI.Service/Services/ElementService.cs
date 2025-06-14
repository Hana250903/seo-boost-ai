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

        /// <summary>
/// Initializes a new instance of the <see cref="ElementService"/> class with a default <see cref="ElementRepository"/>.
/// </summary>
public ElementService() => _repository = new ElementRepository();

        /// <summary>
        /// Analyzes the HTML elements of a web page at the specified URL and associates the results with the given audit ID.
        /// </summary>
        /// <param name="url">The URL of the web page to analyze.</param>
        /// <param name="auditId">The audit ID to associate with the analyzed elements.</param>
        /// <returns>A list of <see cref="Element"/> objects representing the analyzed elements of the page.</returns>
        public async Task<List<Element>> AnalyzePageAsync(string url, int auditId)
        {
            return await HtmlElementAnalyzer.AnalyzeUrlAsync(url, auditId);
        }

        /// <summary>
        /// Asynchronously creates a new Element record in the data store.
        /// </summary>
        /// <param name="element">The Element entity to be created.</param>
        /// <returns>The result of the creation operation, typically the new record's ID or a status code.</returns>
        public async Task<int> CreateElementAsync(Element element)
        {
            return await _repository.CreateAsync(element);
        }

        /// <summary>
        /// Asynchronously creates multiple Element records in the data store.
        /// </summary>
        /// <param name="elements">A list of Element objects to be created.</param>
        /// <returns>The number of elements successfully created.</returns>
        public async Task<int> CreateElementsAsync(List<Element> elements)
        {
            return await _repository.CreateRangeAsync(elements);
        }

        /// <summary>
        /// Asynchronously deletes an Element by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Element to delete.</param>
        /// <returns>True if the Element was successfully deleted; otherwise, false.</returns>
        public async Task<bool> DeleteElementAsync(int id)
        {
            var element = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(element);
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
