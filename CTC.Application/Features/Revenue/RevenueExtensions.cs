using CTC.Application.Features.Revenue.UseCases.DeleteRevenue;
using CTC.Application.Features.Revenue.UseCases.ListRevenues;
using CTC.Application.Features.Revenue.UseCases.RegisterRevenue;
using CTC.Application.Features.Revenue.UseCases.UpdateRevenue;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Revenue
{
    internal static class RevenueExtensions
    {
        public static IServiceCollection AddRevenue(this IServiceCollection services)
        {
            services.AddRegisterRevenue();
            services.AddUpdateRevenue();
            services.AddDeleteRevenue();
            services.AddListRevenues();
            return services;
        }
    }
}
