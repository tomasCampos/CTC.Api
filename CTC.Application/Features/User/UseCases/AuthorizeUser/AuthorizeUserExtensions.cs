using CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase;
using CTC.Application.Features.User.UseCases.AuthorizeUser.Validators;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User.UseCases.AuthorizeUser
{
    internal static class AuthorizeUserExtensions
    {
        public static IServiceCollection AddAuthorizeUser(this IServiceCollection services) 
        {
            services.AddScoped<IRequestValidator<AuthorizeUserInput>, AuthorizeUserRequestValidator>();
            services.AddScoped<IUseCase<AuthorizeUserInput, Output>, AuthorizeUserUseCase>();
            return services;
        }
    }
}
