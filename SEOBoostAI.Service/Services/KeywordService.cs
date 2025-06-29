﻿using SEOBoostAI.Repository.ModelExtensions;
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

        public KeywordService() => _repository = new KeywordRepository();

        public async Task<int> AddAsync(Keyword keyword)
        {
            return await _repository.CreateAsync(keyword);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var keyword = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(keyword);
        }

        public async Task<List<Keyword>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginationResult<List<Keyword>>> GetAllAsync(string keyword, int pageIndex, int pageSize)
        {
            return await _repository.GetAllAsync(keyword, pageIndex, pageSize);
        }

        public async Task<Keyword> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Keyword keyword)
        {
            return await _repository.UpdateAsync(keyword);
        }
    }
}
