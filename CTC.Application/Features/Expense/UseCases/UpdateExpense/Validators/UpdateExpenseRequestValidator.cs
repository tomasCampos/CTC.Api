using CTC.Application.Features.Expense.UseCases.UpdateExpense.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.UpdateExpense.Validators
{
    internal sealed class UpdateExpenseRequestValidator : IRequestValidator<UpdateExpenseInput>
    {
        public Task<RequestValidationModel> Validate(UpdateExpenseInput request)
        {
            var errors = new List<string>();

            if (!request.Value.HasValue)
                errors.Add("O valor da transação deve ser informado");
            if (string.IsNullOrWhiteSpace(request.CostCenterId))
                errors.Add("O Centro de Custo deve ser informado");
            if (string.IsNullOrWhiteSpace(request.SupplierId))
                errors.Add("O Fornecedor deve ser informado");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
