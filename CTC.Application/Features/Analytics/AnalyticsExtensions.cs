using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Analytics
{
    public static class AnalyticsExtensions
    {
        public static IServiceCollection AddAnalytics(this IServiceCollection services) 
        {
            return services;
        }
    }
}
