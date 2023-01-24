using CTC.Application.Features.User.UseCases.GetUser.Data;
using CTC.Application.Features.User.UseCases.GetUser.UseCase;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User.UseCases.GetUser
{
    internal static class GetUserExtensions
    {
        public static IServiceCollection AddGetUser(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<IGetUserInput, GetUserOutput>, GetUserUseCase>();
            services.AddScoped<IGetUserRepository, GetUserRepository>();
            return services;
        }
    }
}
