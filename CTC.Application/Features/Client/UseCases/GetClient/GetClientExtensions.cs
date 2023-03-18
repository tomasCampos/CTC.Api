using CTC.Application.Features.Client.UseCases.GetClient.Data;
using CTC.Application.Features.Client.UseCases.GetClient.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client.UseCases.GetClient
{
    internal static class GetClientExtensions
    {
        public static IServiceCollection AddGetClient(this IServiceCollection services)
        {
            services.AddScoped<IGetClientRepository, GetClientRepository>();
            services.AddScoped<IUseCase<GetClientInput, Output>, GetClientUseCase>();
            return services;
        }
    }
}
