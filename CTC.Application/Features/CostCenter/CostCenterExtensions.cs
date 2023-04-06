using CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter;
using CTC.Application.Features.CostCenter.UseCases.GetCostCenter;
using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter;
using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter
{
    internal static class CostCenterExtensions
    {
        public static IServiceCollection AddCostCenter(this IServiceCollection services) 
        {
            services.AddRegisterCostCenter();
            services.AddUpdateCostCenter();
            services.AddGetCostCenter();
            services.AddDeleteCostCenter();
            return services;
        }
    }
}
