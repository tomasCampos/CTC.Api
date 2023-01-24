using CTC.Application.Features.Category.UseCases.RegisterCategory.Data;
using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase;
using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase.IO;
using CTC.Application.Features.Category.UseCases.RegisterCategory.Validators;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category.UseCases.RegisterCategory
{
    internal static class RegisterCategoryExtensions
    {
        public static IServiceCollection AddRegisterCategory(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidator<RegisterCategoryInput>, RegisterCategoryRequestValidator>();
            services.AddScoped<IUseCase<RegisterCategoryInput, RegisterCategoryOutput>, RegisterCategoryUseCase>();
            services.AddScoped<IRegisterCategoryRepository, RegisterCategoryRepository>();
            return services;
        }
    }
}