using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly IKeywordService _service;

        public KeywordsController(IKeywordService service)
        {
            _service = service;
        }

        // GET: api/<KeywordsController>
        [HttpGet]
        public async Task<List<Keyword>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<KeywordsController>/5
        [HttpGet("{id}")]
        public async Task<Keyword> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/<KeywordsController>
        [HttpPost]
        public async Task<int> Post(Keyword keyword)
        {
            return await _service.AddAsync(keyword);
        }

        // PUT api/<KeywordsController>/5
        [HttpPut]
        public async Task<int> Put(Keyword keyword)
        {
            return await _service.UpdateAsync(keyword);
        }

        /// <summary>
        /// Deletes a keyword entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the keyword to delete.</param>
        /// <returns>True if the keyword was successfully deleted; otherwise, false.</returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves a list of keywords that match the specified search term.
        /// </summary>
        /// <param name="keyword">The search term to filter keywords.</param>
        /// <returns>A list of matching <see cref="Keyword"/> objects.</returns>
        [HttpGet]
        [Route("search/{keyword}")]
        public async Task<List<Keyword>> Get(string keyword)
        {
            return await _service.GetAllAsync(keyword);
        }
    }
}
