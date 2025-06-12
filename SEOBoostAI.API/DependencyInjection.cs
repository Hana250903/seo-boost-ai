using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;

namespace SEOBoostAI.API
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers core web API services and their implementations with the dependency injection container.
        /// </summary>
        /// <returns>The updated <see cref="IServiceCollection"/> with all required services registered.</returns>
        public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IRankTrackingService, RankTrackingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuditReportService, AuditReportService>();
            services.AddScoped<IContentOptimizationService, ContentOptimizationService>();
            services.AddScoped<IKeywordService, KeywordService>();
            services.AddScoped<IElementService, ElementService>();
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<UserRepository>();
            services.AddHttpClient();
            return services;
        }
    }
}
