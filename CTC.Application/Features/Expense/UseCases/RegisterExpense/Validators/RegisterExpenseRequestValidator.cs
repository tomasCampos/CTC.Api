using CTC.Application.Features.Expense.UseCases.RegisterExpense.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.RegisterExpense.Validators
{
    internal sealed class RegisterExpenseRequestValidator : IRequestValidator<RegisterExpenseInput>
    {
        public Task<RequestValidationModel> Validate(RegisterExpenseInput request)
        {
            throw new System.NotImplementedException();
            //TODO: Implementar validator e caso de uso de registrar expense. 
        }
    }
}
