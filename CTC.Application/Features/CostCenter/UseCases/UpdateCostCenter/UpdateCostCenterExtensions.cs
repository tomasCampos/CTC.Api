using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.Data;
using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter
{
    internal static class UpdateCostCenterExtensions
    {
        public static IServiceCollection AddUpdateCostCenter(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<UpdateCostCenterInput, Output>, UpdateCostCenterUseCase>();
            services.AddScoped<IRequestValidator<UpdateCostCenterInput>, UpdateCostCenterRequestValidator>();
            services.AddScoped<IUpdateCostCenterRepository, UpdateCostCenterRepository>();

            return services;
        }
    }
}
