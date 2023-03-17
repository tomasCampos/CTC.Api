using CTC.Application.Features.Supplier.UseCases.GetSupplier.Data;
using CTC.Application.Features.Supplier.UseCases.GetSupplier.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Supplier.UseCases.GetSupplier
{
    internal static class GetSupplierExtensions
    {
        public static IServiceCollection AddGetSupplier(this IServiceCollection services) 
        {
            services.AddScoped<IGetSupplierRepository, GetSupplierRepository>();
            services.AddScoped<IUseCase<GetSupplierInput, Output>, GetSupplierUseCase>();
            return services;
        }
    }
}
