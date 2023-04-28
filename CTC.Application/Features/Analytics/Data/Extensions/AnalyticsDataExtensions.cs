using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Analytics.Data.Extensions
{
    internal static class AnalyticsDataExtensions
    {
        public static IServiceCollection AddAnalyticsData(this IServiceCollection services)
        {
            services.AddScoped<ITransactionAnalyticsRepository, TransactionAnalyticsRepository>();
            return services;
        }
    }
}
