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
        /// <summary>
/// Retrieves a paginated list of Element entities.
/// </summary>
/// <param name="currentPage">The page number to retrieve.</param>
/// <param name="pageSize">The number of elements per page.</param>
/// <returns>A task that represents the asynchronous operation, containing a PaginationResult with a list of Element objects for the specified page.</returns>
Task<PaginationResult<List<Element>>> GetElementWithPaginateAsync(int currentPage, int pageSize);
        /// <summary>
/// Retrieves an Element by its unique identifier asynchronously.
/// </summary>
/// <param name="id">The unique identifier of the Element to retrieve.</param>
/// <returns>The Element with the specified ID, or null if not found.</returns>
Task<Element> GetElementByIdAsync(int id);
        /// <summary>
/// Asynchronously creates a new Element entity.
/// </summary>
/// <param name="element">The Element to create.</param>
/// <returns>The ID of the newly created Element.</returns>
Task<int> CreateElementAsync(Element element);
        /// <summary>
/// Creates multiple Element entities asynchronously.
/// </summary>
/// <param name="elements">The list of Element objects to create.</param>
/// <returns>The number of elements successfully created.</returns>
Task<int> CreateElementsAsync(List<Element> elements);
        /// <summary>
/// Updates an existing Element entity.
/// </summary>
/// <param name="element">The Element object containing updated data.</param>
/// <returns>The number of records affected by the update operation.</returns>
Task<int> UpdateElementAsync(Element element);
        /// <summary>
/// Deletes an Element by its unique identifier.
/// </summary>
/// <param name="id">The ID of the Element to delete.</param>
/// <returns>True if the Element was successfully deleted; otherwise, false.</returns>
Task<bool> DeleteElementAsync(int id);
        /// <summary>
/// Analyzes the specified web page and returns a list of extracted Element objects.
/// </summary>
/// <param name="url">The URL of the web page to analyze.</param>
/// <param name="auditId">The identifier for the associated audit.</param>
/// <returns>A task representing the asynchronous operation, containing a list of Element objects found during analysis.</returns>
Task<List<Element>> AnalyzePageAsync(string url, int auditId);
    }
}
