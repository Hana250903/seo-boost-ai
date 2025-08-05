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
    public class AccountTypesController : ControllerBase
    {
        private readonly IAccountTypeService _accountTypeService;

        public AccountTypesController(IAccountTypeService accountTypeService)
        {
            _accountTypeService = accountTypeService;
        }

        // GET: api/<AccountTypesController>
        [HttpGet]
        public async Task<IEnumerable<AccountType>> Get()
        {
            return await _accountTypeService.GetAllAsync();
        }

        // GET api/<AccountTypesController>/5
        [HttpGet("{id}")]
        public async Task<AccountType> Get(int id)
        {
            return await _accountTypeService.GetByIdAsync(id);
        }

        // POST api/<AccountTypesController>
        [HttpPost]
        public async Task<int> Post([FromBody] AccountType accountType)
        {
            return await _accountTypeService.AddAsync(accountType);
        }

        // PUT api/<AccountTypesController>/5
        [HttpPut]
        public async Task<int> Put([FromBody] AccountType accountType)
        {
            return await _accountTypeService.UpdateAsync(accountType);
        }

        // DELETE api/<AccountTypesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _accountTypeService.DeleteAsync(id);
        }
    }
}
