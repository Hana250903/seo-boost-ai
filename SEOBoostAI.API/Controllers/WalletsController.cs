using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        // GET: api/<WalletsController>
        [HttpGet]
        public async Task<PaginationResult<List<Wallet>>> Get(int pageIndex, int pageSize, string? sortOrder)
        {
            return await _walletService.GetWallet(pageIndex, pageSize, sortOrder);
        }

        // GET api/<WalletsController>/5
        [HttpGet("{id}")]
        public async Task<Wallet> Get(int id)
        {
            return await _walletService.GetWalletByIdAsync(id);
        }

        // POST api/<WalletsController>
        [HttpPost]
        public async Task<int> Post([FromBody] Wallet wallet)
        {
            return await _walletService.CreateWalletAsync(wallet);
        }

        // PUT api/<WalletsController>/5
        [HttpPut]
        public async Task<int> Put([FromBody] Wallet wallet)
        {
            return await _walletService.UpdateWalletAsync(wallet);
        }

        // DELETE api/<WalletsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _walletService.DeleteWalletAsync(id);
        }

        [HttpGet("user/{userId}")]
        public async Task<Wallet> GetUserWallets(int userId)
        {
            return await _walletService.GetUserWalletsAsync(userId);
        }
    }
}
