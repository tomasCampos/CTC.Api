using CTC.Application.Shared.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Shared
{
    internal static class SharedExtensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddScoped<IMySqlDataBaseConnector, MySqlDataBaseConnector>();
            return services;
        }
    }
}
