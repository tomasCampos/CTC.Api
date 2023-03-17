using CTC.Application.Features.Client.UseCases.ListClients.Data;
using CTC.Application.Features.Client.UseCases.ListClients.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client.UseCases.ListClients
{
    internal static class ListClientsExtensions
    {
        public static IServiceCollection AddListClients(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<ListClientsInput, Output>, ListClientsUseCase>();
            services.AddScoped<IListClientsRepository, ListClientsRepository>();
            return services;
        }
    }
}
