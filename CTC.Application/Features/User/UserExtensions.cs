using CTC.Application.Features.User.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User
{
    internal static class UserExtensions
    {
        public static IServiceCollection AddUser(this IServiceCollection services)
        {
            services.AddRegisterUser();
            return services;
        }
    }
}
