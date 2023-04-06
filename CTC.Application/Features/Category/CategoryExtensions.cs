using CTC.Application.Features.Category.UseCases.DeleteCategory;
using CTC.Application.Features.Category.UseCases.GetCategory;
using CTC.Application.Features.Category.UseCases.ListCategories;
using CTC.Application.Features.Category.UseCases.RegisterCategory;
using CTC.Application.Features.Category.UseCases.UpdateCategory;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category
{
    internal static class CategoryExtensions
    {
        public static IServiceCollection AddCategory(this IServiceCollection services)
        {
            services.AddRegisterCategory();
            services.AddListCategories();
            services.AddGetCategory();
            services.AddUpdateCategory();
            services.AddDeleteCategory();
            return services;
        }
    }
}
