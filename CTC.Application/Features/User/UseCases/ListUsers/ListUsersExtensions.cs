using CTC.Application.Features.User.UseCases.ListUsers.Data;
using CTC.Application.Features.User.UseCases.ListUsers.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User.UseCases.ListUsers
{
    internal static class ListUsersExtensions
    {
        public static IServiceCollection AddListUsers(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<ListUsersUseCaseInput, Output>, ListUsersUseCase>();
            services.AddScoped<IListUsersRepository, ListUsersRepository>();
            return services;
        }
    }
}
