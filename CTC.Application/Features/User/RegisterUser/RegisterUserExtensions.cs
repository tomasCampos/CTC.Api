using CTC.Application.Features.User.RegisterUser.Repositories;
using CTC.Application.Features.User.RegisterUser.UseCase;
using CTC.Application.Features.User.RegisterUser.UseCase.IO;
using CTC.Application.Features.User.RegisterUser.Validators;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User.RegisterUser
{
    internal static class RegisterUserExtensions
    {
        public static IServiceCollection AddRegisterUser(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidator<RegisterUserInput>, RegisterUserRequestValidator>();
            services.AddScoped<IUseCase<RegisterUserInput, RegisterUserOutput>, RegisterUserUseCase>();
            services.AddScoped<IRegisterUserRepository, RegisterUserRepository>();
            return services;
        }
    }
}
