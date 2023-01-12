using CTC.Application.Features.Category;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCategory();
            return services;
        }
    }
}
