﻿using CTC.Application.Features.User.UseCases.GetUser;
using CTC.Application.Features.User.UseCases.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.User
{
    internal static class UserExtensions
    {
        public static IServiceCollection AddUser(this IServiceCollection services)
        {
            services.AddRegisterUser();
            services.AddGetUser();
            return services;
        }
    }
}
