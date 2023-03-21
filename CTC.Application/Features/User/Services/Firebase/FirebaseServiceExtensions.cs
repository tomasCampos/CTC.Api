using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User.Services.Firebase
{
    internal static class FirebaseServiceExtensions
    {
        public static IServiceCollection AddFirebaseService(this IServiceCollection services)
        {
            services.AddSingleton<IFirebaseService, FirebaseService>();
            return services;
        }
    }
}
