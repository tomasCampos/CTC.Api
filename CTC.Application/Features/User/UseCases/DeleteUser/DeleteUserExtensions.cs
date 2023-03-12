using CTC.Application.Features.User.UseCases.DeleteUser.Data;
using CTC.Application.Features.User.UseCases.DeleteUser.UseCase;
using CTC.Application.Features.User.UseCases.DeleteUser.Validators;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User.UseCases.DeleteUser
{
    internal static class DeleteUserExtensions
    {
        public static IServiceCollection AddDeleteUser(this IServiceCollection services) 
        {
            services.AddScoped<IRequestValidator<DeleteUserInput>, DeleteUserRequestValidator>();
            services.AddScoped<IDeleteUserRepository, DeleteUserRepository>();
            services.AddScoped<IUseCase<DeleteUserInput, Output>, DeleteUserUseCase>();
            return services;
        }
    }
}
