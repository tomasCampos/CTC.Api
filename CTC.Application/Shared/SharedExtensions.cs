using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Data;
using CTC.Application.Shared.UserContext;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Shared
{
    internal static class SharedExtensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddScoped<ISqlService, SqlService>();
            services.AddSingleton<IDataContext, MySqlDataContext>();
            services.AddScoped<IUseCaseAuthorizationService, UseCaseAuthorizationService>();
            services.AddUserContext();
            return services;
        }
    }
}
