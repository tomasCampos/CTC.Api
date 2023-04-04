using CTC.Application.Features.Category;
using CTC.Application.Features.Client;
using CTC.Application.Features.CostCenter;
using CTC.Application.Features.Supplier;
using CTC.Application.Features.User;
using CTC.Application.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCategory();
            services.AddUser();
            services.AddSupplier();
            services.AddClient();
            services.AddCostCenter();
            services.AddShared();
            return services;
        }
    }
}
