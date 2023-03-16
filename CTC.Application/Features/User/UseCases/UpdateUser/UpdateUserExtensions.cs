using CTC.Application.Features.User.UseCases.UpdateUser.Data;
using CTC.Application.Features.User.UseCases.UpdateUser.UseCase;
using CTC.Application.Features.User.UseCases.UpdateUser.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User.UseCases.UpdateUser
{
    internal static class UpdateUserExtensions
    {
        public static IServiceCollection AddUpdateUser(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidator<UpdateUserInput>, UpdateUserRequestValidator>();
            services.AddScoped<IUseCase<UpdateUserInput, Output>, UpdateUserUseCase>();
            services.AddScoped<IUpdateUserRepository, UpdateUserRepository>();
            return services;
        }
    }
}
