using CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.Data;
using CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport
{
    internal static class GetCostCenterReportExtensions
    {
        public static IServiceCollection AddGetCostCenterReport(this IServiceCollection services) 
        {
            services.AddScoped<IGetCostCenterReportRepository, GetCostCenterReportRepository>();
            services.AddScoped<IUseCase<GetCostCenterReportInput, Output>, GetCostCenterReportUseCase>();
            return services;
        }
    }
}
