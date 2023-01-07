using CTC.Application.Features.Category.RegisterCategory.Repositories;
using CTC.Application.Features.Category.RegisterCategory.UseCase;
using CTC.Application.Features.Category.RegisterCategory.UseCase.IO;
using CTC.Application.Features.Category.RegisterCategory.Validators;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category.RegisterCategory
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