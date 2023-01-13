using CTC.Application.Features.Category;
using CTC.Application.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCategory();
            services.AddShared();
            return services;
        }
    }
}
