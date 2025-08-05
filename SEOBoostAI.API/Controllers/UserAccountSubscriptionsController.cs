using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountSubscriptionsController : ControllerBase
    {
        private readonly IUserAccountSubscriptionService _userAccountSubscription;

        public UserAccountSubscriptionsController(IUserAccountSubscriptionService userAccountSubscription)
        {
            _userAccountSubscription = userAccountSubscription;
        }

        // GET: api/<UserAccountSubscriptionsController>
        [HttpGet]
        public async Task<IEnumerable<UserAccountSubscription>> Get()
        {
            return await _userAccountSubscription.GetAllAsync();
        }

        // GET api/<UserAccountSubscriptionsController>/5
        [HttpGet("{id}")]
        public async Task<UserAccountSubscription> Get(int id)
        {
            return await _userAccountSubscription.GetByIdAsync(id);
        }

        // POST api/<UserAccountSubscriptionsController>
        [HttpPost]
        public async Task<int> Post([FromBody] UserAccountSubscription userAccountSubscription)
        {
            return await _userAccountSubscription.AddAsync(userAccountSubscription);
        }

        // PUT api/<UserAccountSubscriptionsController>/5
        [HttpPut]
        public async Task<int> Put([FromBody] UserAccountSubscription userAccountSubscription)
        {
            return await _userAccountSubscription.UpdateAsync(userAccountSubscription);
        }

        // DELETE api/<UserAccountSubscriptionsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _userAccountSubscription.DeleteAsync(id);
        }
    }
}
