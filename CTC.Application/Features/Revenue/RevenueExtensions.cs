using CTC.Application.Features.Revenue.UseCases.RegisterRevenue;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Revenue
{
    internal static class RevenueExtensions
    {
        public static IServiceCollection AddRevenue(this IServiceCollection services)
        {
            services.AddRegisterRevenue();
            return services;
        }
    }
}
