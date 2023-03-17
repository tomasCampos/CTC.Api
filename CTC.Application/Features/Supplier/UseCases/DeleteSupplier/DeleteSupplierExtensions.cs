using CTC.Application.Features.Supplier.UseCases.DeleteSupplier.Data;
using CTC.Application.Features.Supplier.UseCases.DeleteSupplier.UseCase;
using CTC.Application.Features.Supplier.UseCases.DeleteSupplier.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Supplier.UseCases.DeleteSupplier
{
    internal static class DeleteSupplierExtensions
    {
        public static IServiceCollection AddDeleteSupplier(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidator<DeleteSupplierInput>, DeleteSupplierRequestValidator>();
            services.AddScoped<IDeleteSupplierRepository, DeleteSupplierRepository>();
            services.AddScoped<IUseCase<DeleteSupplierInput, Output>, DeleteSupplierUseCase>();
            return services;
        }
    }
}
