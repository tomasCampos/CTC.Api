using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Data;
using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier
{
    internal static class RegisterSupplierExtensions
    {
        public static IServiceCollection AddRegisterSupplier(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<RegisterSupplierInput, Output>>();
            services.AddScoped<IRequestValidator<RegisterSupplierInput>>();
            services.AddScoped<IRegisterSupplierRepository, RegisterSupplierRepository>();

            return services;
        }
    }
}
