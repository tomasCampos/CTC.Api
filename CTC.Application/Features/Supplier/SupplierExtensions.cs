using CTC.Application.Features.Supplier.UseCases.DeleteSupplier;
using CTC.Application.Features.Supplier.UseCases.ListSuppliers;
using CTC.Application.Features.Supplier.UseCases.RegisterSupplier;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Supplier
{
    internal static class SupplierExtensions
    {
        public static IServiceCollection AddSupplier(this IServiceCollection services) 
        {
            services.AddRegisterSupplier();
            services.AddListSuppliers();
            services.AddDeleteSupplier();
            return services;
        }
    }
}
