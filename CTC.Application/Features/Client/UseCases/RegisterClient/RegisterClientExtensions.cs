using CTC.Application.Features.Client.UseCases.RegisterClient.Data;
using CTC.Application.Features.Client.UseCases.RegisterClient.UseCase;
using CTC.Application.Features.Client.UseCases.RegisterClient.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client.UseCases.RegisterClient
{
    internal static class RegisterClientExtensions
    {
        public static IServiceCollection AddRegisterClient(this IServiceCollection services) 
        {
            services.AddScoped<IUseCase<RegisterClientInput, Output>, RegisterClientUseCase>();
            services.AddScoped<IRequestValidator<RegisterClientInput>, RegisterClientRequestValidator>();
            services.AddScoped<IRegisterClientRepository, RegisterClientRepository>();

            return services;
        }
    }
}
