using CTC.Application.Features.Analytics.UseCases.GetOverview.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Analytics.UseCases.GetOverview
{
    internal static class GetOverviewExtensions
    {
        public static IServiceCollection AddGetOverview(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<GetOverviewInput, Output>, GetOverviewUseCase>();
            return services;
        }
    }
}
