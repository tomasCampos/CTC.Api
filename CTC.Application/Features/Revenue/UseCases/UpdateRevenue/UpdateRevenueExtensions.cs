using CTC.Application.Features.Revenue.UseCases.UpdateRevenue.Data;
using CTC.Application.Features.Revenue.UseCases.UpdateRevenue.UseCase;
using CTC.Application.Features.Revenue.UseCases.UpdateRevenue.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Revenue.UseCases.UpdateRevenue
{
    internal static class UpdateRevenueExtensions
    {
        public static IServiceCollection AddUpdateRevenue(this IServiceCollection services)
        {
            services.AddScoped<IUpdateRevenueRepository, UpdateRevenueRepository>();
            services.AddScoped<IRequestValidator<UpdateRevenueInput>, UpdateRevenueRequestValidator>();
            services.AddScoped<IUseCase<UpdateRevenueInput, Output>, UpdateRevenueUseCase>();
            return services;
        }
    }
}
