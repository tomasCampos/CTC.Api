using CTC.Application.Features.Client.UseCases.UpdateClient.Data;
using CTC.Application.Features.Client.UseCases.UpdateClient.UseCase;
using CTC.Application.Features.Client.UseCases.UpdateClient.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client.UseCases.UpdateClient
{
    internal static class UpdateClientExtensions
    {
        public static IServiceCollection AddUpdateClient(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<UpdateClientInput, Output>, UpdateClientUseCase>();
            services.AddScoped<IRequestValidator<UpdateClientInput>, UpdateClientRequestValidator>();
            services.AddScoped<IUpdateClientRepository, UpdateClientRepository>();
            return services;
        }
    }
}
