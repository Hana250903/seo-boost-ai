﻿using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;

namespace SEOBoostAI.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IRankTrackingService, RankTrackingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuditReportService, AuditReportService>();
            services.AddScoped<IContentOptimizationService, ContentOptimizationService>();
            services.AddScoped<IKeywordService, KeywordService>();
            services.AddScoped<IElementService, ElementService>();
            return services;
        }
    }
}
