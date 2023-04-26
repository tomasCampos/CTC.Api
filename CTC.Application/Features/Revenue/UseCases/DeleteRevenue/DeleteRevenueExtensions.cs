using CTC.Application.Features.Revenue.UseCases.DeleteRevenue.Data;
using CTC.Application.Features.Revenue.UseCases.DeleteRevenue.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Revenue.UseCases.DeleteRevenue
{
    internal static class DeleteRevenueExtensions
    {
        public static IServiceCollection AddDeleteRevenue(this IServiceCollection services)
        {
            services.AddScoped<IDeleteRevenueRepository, DeleteRevenueRepository>();
            services.AddScoped<IUseCase<DeleteRevenueInput, Output>, DeleteRevenueUseCase>();
            return services;
        }
    }
}
