using CTC.Application.Features.CostCenter.UseCases.ListCostCenter.Data;
using CTC.Application.Features.CostCenter.UseCases.ListCostCenter.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter.UseCases.ListCostCenter
{
    internal static class ListCostCentersExtensions
    {
        public static IServiceCollection AddListCostCenters(this IServiceCollection services) 
        {
            services.AddScoped<IListCostCentersRepository, ListCostCentersRepository>();
            services.AddScoped<IUseCase<ListCostCentersInput, Output>, ListCostCentersUseCase>();
            return services;
        }
    }
}
