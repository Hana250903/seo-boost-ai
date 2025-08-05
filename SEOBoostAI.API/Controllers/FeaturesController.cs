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
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        // GET: api/<FeaturesController>
        [HttpGet]
        public async Task<IEnumerable<Feature>> Get()
        {
            return await _featureService.GetAllAsync();
        }

        // GET api/<FeaturesController>/5
        [HttpGet("{id}")]
        public async Task<Feature> Get(int id)
        {
            return await _featureService.GetByIdAsync(id);
        }

        // POST api/<FeaturesController>
        [HttpPost]
        public async Task<int> Post([FromBody] Feature feature)
        {
            return await _featureService.AddAsync(feature);
        }

        // PUT api/<FeaturesController>/5
        [HttpPut]
        public async Task<int> Put([FromBody] Feature feature)
        {
            return await _featureService.UpdateAsync(feature);
        }

        // DELETE api/<FeaturesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _featureService.DeleteAsync(id);
        }
    }
}
