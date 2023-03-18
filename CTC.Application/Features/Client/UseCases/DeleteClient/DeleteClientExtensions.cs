using CTC.Application.Features.Client.UseCases.DeleteClient.Data;
using CTC.Application.Features.Client.UseCases.DeleteClient.UseCase;
using CTC.Application.Features.Client.UseCases.DeleteClient.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client.UseCases.DeleteClient
{
    internal static class DeleteClientExtensions
    {
        public static IServiceCollection AddDeleteClient(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidator<DeleteClientInput>, DeleteClientRequestValidator>();
            services.AddScoped<IDeleteClientRepository, DeleteClientRepository>();
            services.AddScoped<IUseCase<DeleteClientInput, Output>, DeleteClientUseCase>();
            return services;
        }
    }
}
