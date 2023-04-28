using CTC.Application.Features.Analytics.UseCases.GetCashFlow.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Analytics.UseCases.CashFlow
{
    internal static class GetCashFlowExtensions
    {
        public static IServiceCollection AddGetCashFLow(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<GetCashFlowInput, Output>, GetCashFlowUseCase>();
            return services;
        }
    }
}
