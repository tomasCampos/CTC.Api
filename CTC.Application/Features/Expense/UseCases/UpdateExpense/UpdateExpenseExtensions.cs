using CTC.Application.Features.Expense.UseCases.UpdateExpense.Data;
using CTC.Application.Features.Expense.UseCases.UpdateExpense.UseCase;
using CTC.Application.Features.Expense.UseCases.UpdateExpense.Validators;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application.Features.Expense.UseCases.UpdateExpense
{
    internal static class UpdateExpenseExtensions
    {
        public static IServiceCollection AddUpdateExpense(this IServiceCollection services)
        {
            services.AddScoped<IUpdateExpenseRepository, UpdateExpenseRepository>();
            services.AddScoped<IRequestValidator<UpdateExpenseInput>, UpdateExpenseRequestValidator>();
            services.AddScoped<IUseCase<UpdateExpenseInput, Output>, UpdateExpenseUseCase>();
            return services;
        }
    }
}
