using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentOptimizationsController : ControllerBase
    {
        private readonly IContentOptimizationService _service;

        public ContentOptimizationsController(IContentOptimizationService service)
        {
            _service = service;
        }

        // GET: api/<ContentOptimizationsController>
        [HttpGet]
        public async Task<List<ContentOptimization>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<ContentOptimizationsController>/5
        [HttpGet("{id}")]
        public async Task<ContentOptimization> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/<ContentOptimizationsController>
        [HttpPost]
        public async Task<int> Post(ContentOptimization contentOptimization)
        {
            return await _service.AddAsync(contentOptimization);
        }

        // PUT api/<ContentOptimizationsController>/5
        [HttpPut]
        public async Task<int> Put(ContentOptimization contentOptimization)
        {
            return await _service.UpdateAsync(contentOptimization);
        }

        // DELETE api/<ContentOptimizationsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
