using CTC.Application.Features.Client.UseCases.DeleteClient;
using CTC.Application.Features.Client.UseCases.GetClient;
using CTC.Application.Features.Client.UseCases.ListClients;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client
{
    internal static class ClientExtensions
    {
        public static IServiceCollection AddClient(this IServiceCollection services)
        {
            services.AddDeleteClient();
            services.AddListClients();
            services.AddGetClient();
            return services;
        }
    }
}
