using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Transaction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.DeleteExpense.Data
{
    internal sealed class DeleteExpenseRepository : IDeleteExpenseRepository
    {
        private readonly ISqlService _sqlService;

        public DeleteExpenseRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<string> GetTransactionIdByExpenseId(string id)
        {
            var transactionId = await _sqlService.SelectAsync<string>(ExpenseSqlScripts.SELECT_TRANSACTION_BY_EXPENSE_ID, new { expense_id = id });
            return transactionId.FirstOrDefault();
        }

        public async Task<bool> DeleteExpense(string expenseId, string transactionId)
        {
            var commands = BuildCommands(expenseId, transactionId);
            
            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(string expenseId, string transactionId)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    ExpenseSqlScripts.DELETE_EXPENSE,
                    new
                    {
                        expense_id = expenseId
                    }
                },
                {
                    TransactionSqlScripts.DELETE_TRANSACTION,
                    new
                    {
                        transaction_id = transactionId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
