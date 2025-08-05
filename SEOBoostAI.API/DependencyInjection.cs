using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;

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
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<IUserAccountSubscriptionService, UserAccountSubscriptionService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<WalletRepository>();
            services.AddScoped<FeatureRepository>();
            services.AddScoped<TransactionRepository>();
            services.AddScoped<AccountTypeRepository>();
            services.AddScoped<UserAccountSubscriptionRepository>();
            services.AddHttpClient();
            return services;
        }
    }
}
