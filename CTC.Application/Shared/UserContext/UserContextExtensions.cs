using CTC.Application.Shared.UserContext.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Shared.UserContext
{
    internal static class UserContextExtensions
    {
        public static IServiceCollection AddUserContext(this IServiceCollection services) 
        {
            services.AddScoped<UserContext>();
            services.AddScoped<IUserContext>(provider => provider.GetRequiredService<UserContext>());
            services.AddScoped<IUserContextSet>(provider => provider.GetRequiredService<UserContext>());

            services.AddScoped<UserContextService>();
            services.AddScoped<IUserContextService>(provider => provider.GetRequiredService<UserContextService>());
            services.AddScoped<IUserContextCacheReset>(provider => provider.GetRequiredService<UserContextService>());
            return services;
        }
    }
}
