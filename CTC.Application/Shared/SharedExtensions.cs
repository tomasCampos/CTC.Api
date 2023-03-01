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
            AddUserContext(services);
            return services;
        }

        private static void AddUserContext(IServiceCollection services)
        {
            services.AddScoped<UserContext.UserContext>();
            services.AddScoped<IUserContext>(provider => provider.GetRequiredService<UserContext.UserContext>());
            services.AddScoped<IUserContextSet>(provider => provider.GetRequiredService<UserContext.UserContext>());
        }
    }
}
