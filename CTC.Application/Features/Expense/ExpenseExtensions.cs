using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense
{
    internal static class ExpenseExtensions
    {
        public static IServiceCollection AddExpense(this IServiceCollection services) 
        {
            return services;
        }
    }
}
