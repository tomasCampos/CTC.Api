using CTC.Application.Features.CostCenter.UseCases.GetCostCenter.Data;
using CTC.Application.Features.CostCenter.UseCases.GetCostCenter.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenter
{
    internal static class GetCostCenterExtensions
    {
        public static IServiceCollection AddGetCostCenter(this IServiceCollection services)
        {
            services.AddScoped<IGetCostCenterRepository, GetCostCenterRepository>();
            services.AddScoped<IUseCase<GetCostCenterInput, Output>, GetCostCenterUseCase>();

            return services;
        }
    }
}
