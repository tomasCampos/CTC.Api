using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense.UseCases.RegisterExpense
{
    internal static class RegisterExpenseExtensions
    {
        public static IServiceCollection AddRegisterExpense(this IServiceCollection services) 
        {
            return services;
        }
    }
}
