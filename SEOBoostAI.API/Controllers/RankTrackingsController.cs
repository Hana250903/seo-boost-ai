using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankTrackingsController : ControllerBase
    {
        private readonly IRankTrackingService _service;

        public RankTrackingsController(IRankTrackingService service)
        {
            _service = service;
        }

        // GET: api/<RankTrackingsController>
        [HttpGet]
        public async Task<List<RankTracking>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<RankTrackingsController>/5
        [HttpGet("{id}")]
        public async Task<RankTracking> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/<RankTrackingsController>
        [HttpPost]
        public async Task<int> Post(RankTracking rankTracking)
        {
            return await _service.AddAsync(rankTracking);
        }

        // PUT api/<RankTrackingsController>/5
        [HttpPut]
        public async Task<int> Put(RankTracking rankTracking)
        {
            return await _service.UpdateAsync(rankTracking);
        }

        // DELETE api/<RankTrackingsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
