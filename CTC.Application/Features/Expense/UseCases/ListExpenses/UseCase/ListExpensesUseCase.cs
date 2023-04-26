using CTC.Application.Features.Expense.UseCases.ListExpenses.Data;
using CTC.Application.Shared.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Collections.Generic;
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
            var expenses = await _repository.ListExpenses(input.Request, input.CostCenterName, input.CategoryName, input.Year);
            var result = FormatExpenseData(expenses);

            return Output.CreateOkResult(result);
        }

        private static PaginatedQueryResult<object> FormatExpenseData(PaginatedQueryResult<ExpenseModel> data)
        {
            var formatedExpenseList = new List<object>();
            foreach (var expense in data.Results)
            {
                var result = new
                {
                    id = expense.ExpenseId,
                    transactionValue = expense.Value,
                    paymentDate = expense.PaymentDate,
                    observations = expense.Observation,
                    category = new
                    {
                        id = expense.CategoryId,
                        name = expense.CategoryName
                    },
                    costCenter = new
                    {
                        id = expense.CostCenterId,
                        name = expense.CostCenterName
                    },
                    supplier = new
                    {
                        id = expense.SupplierId,
                        name = expense.SupplierName
                    }
                };

                formatedExpenseList.Add(result);
            }

            return new PaginatedQueryResult<object>(formatedExpenseList, data.TotalCount);
        }
    }
}
