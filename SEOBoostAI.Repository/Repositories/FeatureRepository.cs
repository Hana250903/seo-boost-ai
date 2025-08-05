﻿using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.Repositories
{
    public class FeatureRepository: GenericRepository<Feature>
    {
        public FeatureRepository() => _context = new SEOBoostAIContext();
        public FeatureRepository(SEOBoostAIContext context) => _context = context;
    }
}
