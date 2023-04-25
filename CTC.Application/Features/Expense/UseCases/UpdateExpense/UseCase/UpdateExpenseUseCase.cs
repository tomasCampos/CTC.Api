using CTC.Application.Features.Expense.UseCases.UpdateExpense.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.UpdateExpense.UseCase
{
    internal sealed class UpdateExpenseUseCase : IUseCase<UpdateExpenseInput, Output>
    {
        private readonly IRequestValidator<UpdateExpenseInput> _validator;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IUpdateExpenseRepository _repository;

        public UpdateExpenseUseCase(
            IRequestValidator<UpdateExpenseInput> validator,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IUpdateExpenseRepository repository)
        {
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _repository = repository;
        }

        public async Task<Output> Execute(UpdateExpenseInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateExpenseUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var transactionId = await _repository.GetTransactionIdByExpenseId(input.ExpenseId!);
            if (string.IsNullOrEmpty(transactionId))
                return Output.CreateInvalidParametersResult("A despesa a ser alterada não existe.");

            var expense = new ExpenseModel(input.SupplierId!, input.Value!.Value, input.PaymentDate, input.Observation,
                input.CategoryId, input.CostCenterId!, transactionId: transactionId, expenseId: input.ExpenseId);
            var wasExpenseUpdatedWithSuccess = await _repository.UpdateExpense(expense);
            if(!wasExpenseUpdatedWithSuccess)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi posspivel atualizar a despesa. Tente novamente mais tarde.");

            return Output.CreateOkResult();
        }
    }
}
