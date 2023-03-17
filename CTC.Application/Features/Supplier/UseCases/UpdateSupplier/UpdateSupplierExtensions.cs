using CTC.Application.Features.Supplier.UseCases.UpdateSupplier.Data;
using CTC.Application.Features.Supplier.UseCases.UpdateSupplier.UseCase;
using CTC.Application.Features.Supplier.UseCases.UpdateSupplier.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Supplier.UseCases.UpdateSupplier
{
    internal static class UpdateSupplierExtensions
    {
        public static IServiceCollection AddUpdateSupplier(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<UpdateSupplierInput, Output>, UpdateSupplierUseCase>();
            services.AddScoped<IRequestValidator<UpdateSupplierInput>, UpdateSupplierRequestValidator>();
            services.AddScoped<IUpdateSupplierRepository, UpdateSupplierRepository>();
            return services;
        }
    }
}
