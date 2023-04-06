using CTC.Application.Features.Category.UseCases.DeleteCategory.Data;
using CTC.Application.Features.Category.UseCases.DeleteCategory.UseCase;
using CTC.Application.Features.Category.UseCases.DeleteCategory.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category.UseCases.DeleteCategory
{
    internal static class DeleteCategoryExtensions
    {
        public static IServiceCollection AddDeleteCategory(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidator<DeleteCategoryInput>, DeleteCategoryRequestValidator>();
            services.AddScoped<IDeleteCategoryRepository, DeleteCategoryRepository>();
            services.AddScoped<IUseCase<DeleteCategoryInput, Output>, DeleteCategoryUseCase>();
            return services;
        }
    }
}
