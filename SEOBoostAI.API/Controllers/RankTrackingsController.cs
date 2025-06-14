using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Deletes a RankTracking record by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the RankTracking record to delete.</param>
        /// <returns>True if the record was successfully deleted; otherwise, false.</returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves a list of RankTracking records filtered by keyword and user ID.
        /// </summary>
        /// <param name="keyword">The keyword to filter rank tracking records.</param>
        /// <param name="userId">The user ID to filter rank tracking records.</param>
        /// <returns>A list of RankTracking records matching the specified keyword and user ID.</returns>
        [HttpGet]
        [Route("search/{userId}/{keyword}")]
        public async Task<List<RankTracking>> Get(string keyword, int userId)
        {
            return await _service.GetAllAsync(keyword, userId);
        }
    }
}
