using CTC.Application.Features.Expense.UseCases.GetExpense.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.GetExpense.UseCase
{
    internal sealed class GetExpenseUseCase : IUseCase<GetExpenseInput, Output>
    {
        private readonly IGetExpenseRepository _expenseRepository;

        public GetExpenseUseCase(IGetExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Output> Execute(GetExpenseInput input)
        {
            var expense = await _expenseRepository.GetExpense(input.ExpenseId!);

            if (expense == null)
                return Output.CreateNotFoundResult();

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

            return Output.CreateOkResult(result);
        }
    }
}
