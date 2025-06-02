using SEOBoostAI.Repository.Models;
using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services
{
    public class KeywordService : IKeywordService
    {
        private readonly KeywordRepository _repository;

        public KeywordService(KeywordRepository repository)
        {
            _repository = repository;
        }
        public Task<int> AddAsync(Keyword keyword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Keyword>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Keyword> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Keyword keyword)
        {
            throw new NotImplementedException();
        }
    }
}
