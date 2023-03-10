using CTC.Application.Features.User.UseCases.AuthorizeUser;
using CTC.Application.Features.User.UseCases.GetUser;
using CTC.Application.Features.User.UseCases.ListUsers;
using CTC.Application.Features.User.UseCases.RegisterUser;
using CTC.Application.Features.User.UseCases.UpdateUser;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User
{
    internal static class UserExtensions
    {
        public static IServiceCollection AddUser(this IServiceCollection services)
        {
            services.AddRegisterUser();
            services.AddGetUser();
            services.AddAuthorizeUser();
            services.AddListUsers();
            services.AddUpdateUser();
            return services;
        }
    }
}
