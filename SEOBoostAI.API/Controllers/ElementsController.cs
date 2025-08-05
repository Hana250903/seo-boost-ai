using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementsController : ControllerBase
    {
        private readonly IElementService _elementService;
        public ElementsController(IElementService elementService)
        {
            _elementService = elementService;
        }

        [HttpGet]
        public async Task<List<Element>> GetAllElement()
        {
            return await _elementService.GetAllElementAsync();
        }

        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<PaginationResult<List<Element>>> GetAllPagination(int currentPage, int pageSize)
        {
            return await _elementService.GetElementWithPaginateAsync(currentPage, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<Element> GetElementById(int id)
        {
            return await _elementService.GetElementByIdAsync(id);
        }

        [HttpPost]
        public async Task<int> CreateElement(Element Element)
        {
            return await _elementService.CreateElementAsync(Element);
        }

        [HttpPut]
        public async Task<int> UpdateElement(Element Element)
        {
            return await _elementService.UpdateElementAsync(Element);
        }

        /// <summary>
        /// Deletes the Element with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Element to delete.</param>
        /// <returns>True if the Element was successfully deleted; otherwise, false.</returns>
        [HttpDelete]
        public async Task<bool> DeleteElement(int id)
        {
            return await _elementService.DeleteElementAsync(id);
        }

        /// <summary>
        /// Retrieves a list of elements that did not pass the specified audit.
        /// </summary>
        /// <param name="auditId">The identifier of the audit to filter elements by.</param>
        /// <returns>A list of elements that did not pass the given audit.</returns>
        [HttpGet("not-pass/{auditId}")]
        public async Task<List<Element>> GetElementsNotPass (int auditId)
        {
            return await _elementService.ElementNotPass(auditId);
        }
    }
}
