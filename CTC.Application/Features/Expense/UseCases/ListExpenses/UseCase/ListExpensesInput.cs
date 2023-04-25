using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Expense.UseCases.ListExpenses.UseCase
{
    public sealed class ListExpensesInput : QueryInput
    {
        public ListExpensesInput(QueryRequest request, in string? costCenterName, in string? categoryName, in int? year) : base(request)
        {
            CostCenterName = costCenterName;
            CategoryName = categoryName;
            Year = year;
        }

        public string? CostCenterName { get; set; }
        public string? CategoryName { get; set; }
        public int? Year { get; set; }
    }
}
