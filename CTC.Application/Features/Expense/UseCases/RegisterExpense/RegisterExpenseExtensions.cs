using CTC.Application.Features.Expense.UseCases.RegisterExpense.Data;
using CTC.Application.Features.Expense.UseCases.RegisterExpense.UseCase;
using CTC.Application.Features.Expense.UseCases.RegisterExpense.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense.UseCases.RegisterExpense
{
    internal static class RegisterExpenseExtensions
    {
        public static IServiceCollection AddRegisterExpense(this IServiceCollection services) 
        {
            services.AddScoped<IRegisterExpenseRepository, RegisterExpenseRepository>();
            services.AddScoped<IRequestValidator<RegisterExpenseInput>, RegisterExpenseRequestValidator>();
            services.AddScoped<IUseCase<RegisterExpenseInput, Output>, RegisterExpenseUseCase>();
            return services;
        }
    }
}
