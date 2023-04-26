using CTC.Application.Features.Revenue.UseCases.GetRevenue.Data;
using CTC.Application.Features.Revenue.UseCases.GetRevenue.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Revenue.UseCases.GetRevenue
{
    internal static class GetRevenueExtensions
    {
        public static IServiceCollection AddGetRevenue(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<GetRevenueInput, Output>, GetRevenueUseCase>();
            services.AddScoped<IGetRevenueRepository, GetRevenueRepository>();
            return services;
        }
    }
}
