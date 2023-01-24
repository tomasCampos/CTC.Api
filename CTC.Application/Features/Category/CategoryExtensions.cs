using CTC.Application.Features.Category.UseCases.RegisterCategory;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Category
{
    internal static class CategoryExtensions
    {
        public static IServiceCollection AddCategory(this IServiceCollection services)
        {
            services.AddRegisterCategory();
            return services;
        }
    }
}
