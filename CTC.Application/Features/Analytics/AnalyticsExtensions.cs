using CTC.Application.Features.Analytics.Data.Extensions;
using CTC.Application.Features.Analytics.UseCases.CashFlow;
using CTC.Application.Features.Analytics.UseCases.GetCostCenterSummary;
using CTC.Application.Features.Analytics.UseCases.GetOverview;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Analytics
{
    public static class AnalyticsExtensions
    {
        public static IServiceCollection AddAnalytics(this IServiceCollection services) 
        {
            services.AddAnalyticsData();
            services.AddGetOverview();
            services.AddGetCashFLow();
            services.AddGetCostCenterSummary();
            return services;
        }
    }
}
