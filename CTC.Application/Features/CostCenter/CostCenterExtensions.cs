using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter
{
    internal static class CostCenterExtensions
    {
        public static IServiceCollection AddCostCenter(this IServiceCollection services) 
        {
            services.AddRegisterCostCenter();
            return services;
        }
    }
}
