using CTC.Application.Features.Expense.UseCases.DeleteExpense.Data;
using CTC.Application.Features.Expense.UseCases.DeleteExpense.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense.UseCases.DeleteExpense
{
    internal static class DeleteExpenseExtensions
    {
        public static IServiceCollection AddDeleteExpense(this IServiceCollection services)
        {
            services.AddScoped<IDeleteExpenseRepository, DeleteExpenseRepository>();
            services.AddScoped<IUseCase<DeleteExpenseInput, Output>, DeleteExpenseUseCase>();
            return services;
        }
    }
}
