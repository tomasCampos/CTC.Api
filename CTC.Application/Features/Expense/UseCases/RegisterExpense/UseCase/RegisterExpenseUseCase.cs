using CTC.Application.Features.Expense.UseCases.RegisterExpense.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.RegisterExpense.UseCase
{
    internal sealed class RegisterExpenseUseCase : IUseCase<RegisterExpenseInput, Output>
    {
        private readonly IRequestValidator<RegisterExpenseInput> _validator;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IRegisterExpenseRepository _repository;

        public RegisterExpenseUseCase(
            IRequestValidator<RegisterExpenseInput> validator,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IRegisterExpenseRepository repository)
        {
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _repository = repository;
        }

        public async Task<Output> Execute(RegisterExpenseInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterExpenseUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var isSupplierIdValid = await _repository.VerifyIfSupplierExists(input.SupplierId!);
            if (!isSupplierIdValid)
                return Output.CreateInvalidParametersResult("O fornecedor informado não existe.");

            var expense = new ExpenseModel(input.SupplierId!, input.Value!.Value, input.PaymentDate, input.Observation, input.CategoryId, input.CostCenterId!);
            var wasExpenseInsertedWithSuccess = await _repository.InsertExpense(expense);
            if (!wasExpenseInsertedWithSuccess)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi possível cadastrar a despesa. Tente novamente mais tarde.");

            return Output.CreateCreatedResult();
        }
    }
}
