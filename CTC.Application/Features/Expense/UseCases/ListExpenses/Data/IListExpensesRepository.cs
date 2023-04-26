using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.ListExpenses.Data
{
    internal interface IListExpensesRepository
    {
        Task<PaginatedQueryResult<ExpenseModel>> ListExpenses(QueryRequest queryParams, string? costCenterName, string? categoryName, int? year);
    }
}
