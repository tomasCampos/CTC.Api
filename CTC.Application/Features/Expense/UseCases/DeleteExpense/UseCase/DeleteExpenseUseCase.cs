using CTC.Application.Features.Expense.UseCases.DeleteExpense.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.DeleteExpense.UseCase
{
    internal sealed class DeleteExpenseUseCase : IUseCase<DeleteExpenseInput, Output>
    {
        private readonly IDeleteExpenseRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public DeleteExpenseUseCase(IDeleteExpenseRepository repository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(DeleteExpenseInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(DeleteExpenseUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var transactionId = await _repository.GetTransactionIdByExpenseId(input.ExpenseId!);
            if (string.IsNullOrEmpty(transactionId))
                return Output.CreateInvalidParametersResult("A despesa a ser alterada não existe.");

            var result = await _repository.DeleteExpense(input.ExpenseId!, transactionId);
            if(!result)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi posspivel excluir a despesa. Tente novamente mais tarde.");

            return Output.CreateOkResult();
        }
    }
}
