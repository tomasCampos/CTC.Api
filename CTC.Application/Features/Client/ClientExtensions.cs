using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Client
{
    internal static class ClientExtensions
    {
        public static IServiceCollection AddClient(this IServiceCollection services)
        {
            return services;
        }
    }
}
