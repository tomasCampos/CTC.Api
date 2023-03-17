using CTC.Application.Features.Supplier.UseCases.ListSuppliers.Data;
using CTC.Application.Features.Supplier.UseCases.ListSuppliers.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Supplier.UseCases.ListSuppliers
{
    internal static class ListSupplierExtensions
    {
        public static IServiceCollection AddListSuppliers(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<ListSuppliersUseCaseInput, Output>, ListSuppliersUseCase>();
            services.AddScoped<IListSuppliersRepository, ListSuppliersRepository>();
            return services;
        }
    }
}
