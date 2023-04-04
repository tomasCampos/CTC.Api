using CTC.Application.Features.Category.UseCases.ListCategories.Data;
using CTC.Application.Features.Category.UseCases.ListCategories.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category.UseCases.ListCategories
{
    internal static class ListCategoriesExtensions
    {
        public static IServiceCollection AddListCategories(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<ListCategoriesInput, Output>, ListCategoriesUseCase>();
            services.AddScoped<IListCategoriesRepository, ListCategoriesRepository>();
            return services;
        }
    }
}
