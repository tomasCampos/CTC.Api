using CTC.Application.Features.Expense.UseCases.GetExpense.Data;
using CTC.Application.Features.Expense.UseCases.GetExpense.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense.UseCases.GetExpense
{
    internal static class GetExpenseExtensions
    {
        public static IServiceCollection AddGetExpense(this IServiceCollection services) 
        {
            services.AddScoped<IGetExpenseRepository, GetExpenseRepository>();
            services.AddScoped<IUseCase<GetExpenseInput, Output>, GetExpenseUseCase>();
            return services;
        }
    }
}
