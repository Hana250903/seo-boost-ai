using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;

namespace SEOBoostAI.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IRankTrackingService, RankTrackingService>();
            return services;
        }
    }
}
