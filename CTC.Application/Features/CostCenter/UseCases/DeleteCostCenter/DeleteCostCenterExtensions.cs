using CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.Data;
using CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter
{
    internal static class DeleteCostCenterExtensions
    {
        public static IServiceCollection AddDeleteCostCenter(this IServiceCollection services) 
        {
            services.AddScoped<IDeleteCostCenterRepository, DeleteCostCenterRepository>();
            services.AddScoped<IUseCase<DeleteCostCenterInput, Output>, DeleteCostCenterUseCase>();
            return services;
        }
    }
}
