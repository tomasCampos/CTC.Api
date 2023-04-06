using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.Data;
using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter
{
    internal static class RegisterCostCenterExtensions
    {
        public static IServiceCollection AddRegisterCostCenter(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<RegisterCostCenterInput, Output>, RegisterCostCenterUseCase>();
            services.AddScoped<IRequestValidator<RegisterCostCenterInput>, RegisterCostCenterRequestValidator>();
            services.AddScoped<IRegisterCostCenterRepository, RegisterCostCenterRepository>();
            return services;
        }
    }
}
