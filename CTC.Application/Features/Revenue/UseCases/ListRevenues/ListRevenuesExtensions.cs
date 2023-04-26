using CTC.Application.Features.Revenue.UseCases.ListRevenues.Data;
using CTC.Application.Features.Revenue.UseCases.ListRevenues.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Revenue.UseCases.ListRevenues
{
    internal static class ListRevenuesExtensions
    {
        public static IServiceCollection AddListRevenues(this IServiceCollection services)
        {
            services.AddScoped<IListRevenuesRepository, ListRevenuesRepository>();
            services.AddScoped<IUseCase<ListRevenuesInput, Output>, ListRevenuesUseCase>();
            return services;
        }
    }
}
