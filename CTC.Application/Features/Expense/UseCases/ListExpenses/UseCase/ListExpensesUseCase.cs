using CTC.Application.Features.Expense.UseCases.ListExpenses.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.ListExpenses.UseCase
{
    internal class ListExpensesUseCase : IUseCase<ListExpensesInput, Output>
    {
        private readonly IListExpensesRepository _repository;

        public ListExpensesUseCase(IListExpensesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(ListExpensesInput input)
        {
            var result = await _repository.ListExpenses(input.Request, input.CostCenterName, input.CategoryName, input.Year);
            return Output.CreateOkResult(result);
        }
    }
}
