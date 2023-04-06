using CTC.Application.Features.Category.UseCases.UpdateCategory.Data;
using CTC.Application.Features.Category.UseCases.UpdateCategory.UseCase;
using CTC.Application.Features.Category.UseCases.UpdateCategory.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category.UseCases.UpdateCategory
{
    internal static class UpdateCategoryExtensions
    {
        public static IServiceCollection AddUpdateCategory(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidator<UpdateCategoryInput>, UpdateCategoryRequestValidator>();
            services.AddScoped<IUseCase<UpdateCategoryInput, Output>, UpdateCategoryUseCase>();
            services.AddScoped<IUpdateCategoryRepository, UpdateCategoryRepository>();
            return services;
        }
    }
}
