using CTC.Application.Features.Analytics.UseCases.GetCostCenterSummary.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Analytics.UseCases.GetCostCenterSummary
{
    internal static class GetCostCenterSummaryExtensions
    {
        public static IServiceCollection AddGetCostCenterSummary(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<GetCostCenterSummaryInput, Output>, GetCostCenterSummaryUseCase>();
            return services;
        }
    }
}
