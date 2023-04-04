using CTC.Application.Features.Category.UseCases.GetCategory.Data;
using CTC.Application.Features.Category.UseCases.GetCategory.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category.UseCases.GetCategory
{
    internal static class GetCategoryExtensions
    {
        public static IServiceCollection AddGetCategory(this IServiceCollection services) 
        {
            services.AddScoped<IGetCategoryRepository, GetCategoryRepository>();
            services.AddScoped<IUseCase<GetCategoryInput, Output>, GetCategoryUseCase>();
            return services;
        }
    }
}
