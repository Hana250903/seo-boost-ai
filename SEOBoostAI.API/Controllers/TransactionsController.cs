using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.API.VIewModels.Requests;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<PaginationResult<List<Transaction>>> Get(int? userId, string? status, string? type, int pageIndex, int pageSize)
        {
            return await _transactionService.GetAllAsync(userId, status, type, pageIndex, pageSize);
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public async Task<Transaction> Get(int id)
        {
            return await _transactionService.GetByIdAsync(id);
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<int> Post([FromBody] Transaction transaction)
        {
            return await _transactionService.AddAsync(transaction);
        }

        // PUT api/<TransactionsController>/5
        [HttpPut]
        public async Task<int> Put([FromBody] Transaction transaction)
        {
            return await _transactionService.UpdateAsync(transaction);
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _transactionService.DeleteAsync(id);
        }

        [HttpPut("UpdateStatus")]
        public async Task<int> UpdateStatus([FromBody] UpdateTransactionRequest updateTransactionRequest)
        {
            return await _transactionService.UpdateTransactionStatus(updateTransactionRequest.TransactionId, updateTransactionRequest.Status, updateTransactionRequest.Type);
        }
    }
}
