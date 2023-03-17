using CTC.Application.Features.Supplier.UseCases.DeleteSupplier;
using CTC.Application.Features.Supplier.UseCases.ListSuppliers;
using CTC.Application.Features.Supplier.UseCases.RegisterSupplier;
using CTC.Application.Features.Supplier.UseCases.UpdateSupplier;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Supplier
{
    internal static class SupplierExtensions
    {
        public static IServiceCollection AddSupplier(this IServiceCollection services) 
        {
            services.AddRegisterSupplier();
            services.AddListSuppliers();
            services.AddUpdateSupplier();
            services.AddDeleteSupplier();
            return services;
        }
    }
}
