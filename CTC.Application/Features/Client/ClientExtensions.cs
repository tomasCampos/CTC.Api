using CTC.Application.Features.Client.UseCases.RegisterClient;
using CTC.Application.Features.Client.UseCases.UpdateClient;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client
{
    internal static class ClientExtensions
    {
        public static IServiceCollection AddClient(this IServiceCollection services)
        {
            services.AddRegisterClient();
            services.AddUpdateClient();
            return services;
        }
    }
}
