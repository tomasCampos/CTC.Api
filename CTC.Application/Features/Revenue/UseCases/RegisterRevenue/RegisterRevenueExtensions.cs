using CTC.Application.Features.Revenue.UseCases.RegisterRevenue.Data;
using CTC.Application.Features.Revenue.UseCases.RegisterRevenue.UseCase;
using CTC.Application.Features.Revenue.UseCases.RegisterRevenue.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Revenue.UseCases.RegisterRevenue
{
    internal static class RegisterRevenueExtensions
    {
        public static IServiceCollection AddRegisterRevenue(this IServiceCollection services)
        {
            services.AddScoped<IRegisterRevenueRepository, RegisterRevenueRepository>();
            services.AddScoped<IRequestValidator<RegisterRevenueInput>, RegisterRevenueRequestValidator>();
            services.AddScoped<IUseCase<RegisterRevenueInput, Output>, RegisterRevenueUseCase>();
            return services;
        }
    }
}
