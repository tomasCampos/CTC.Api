using CTC.Application.Features.Expense.UseCases.ListExpenses.Data;
using CTC.Application.Features.Expense.UseCases.ListExpenses.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense.UseCases.ListExpenses
{
    internal static class ListExpensesExtensions
    {
        public static IServiceCollection AddListExpense(this IServiceCollection services)
        {
            services.AddScoped<IListExpensesRepository, ListExpensesRepository>();
            services.AddScoped<IUseCase<ListExpensesInput, Output>, ListExpensesUseCase>();
            return services;
        }
    }
}
