using CTC.Application.Features.Expense.UseCases.RegisterExpense;
using CTC.Application.Features.Expense.UseCases.UpdateExpense;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense
{
    internal static class ExpenseExtensions
    {
        public static IServiceCollection AddExpense(this IServiceCollection services) 
        {
            services.AddRegisterExpense();
            services.AddUpdateExpense();
            return services;
        }
    }
}
