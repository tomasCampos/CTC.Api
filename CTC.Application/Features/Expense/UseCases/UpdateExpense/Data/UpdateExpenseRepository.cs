using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Transaction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.UpdateExpense.Data
{
    internal sealed class UpdateExpenseRepository : IUpdateExpenseRepository
    {
        private readonly ISqlService _sqlService;

        public UpdateExpenseRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<string> GetTransactionIdByExpenseId(string id)
        {
            var transactionId = await _sqlService.SelectAsync<string>(ExpenseSqlScripts.SELECT_TRANSACTION_BY_EXPENSE_ID, new { expense_id = id });
            return transactionId.FirstOrDefault();
        }

        public async Task<bool> UpdateExpense(ExpenseModel expense)
        {
            var commands = BuildCommands(expense);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        public async Task<bool> VerifyIfSupplierExists(string supplierId)
        {
            var count = await _sqlService.CountAsync(ExpenseSqlScripts.VERIFY_IF_SUPPLIER_EXISTS, new { supplier_id = supplierId });
            return count > 0;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(ExpenseModel model)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    TransactionSqlScripts.UPDATE_TRANSACTION,
                    new
                    {
                        transaction_id = model.TransactionId,
                        transaction_value = model.Value,
                        transaction_payment_date = model.PaymentDate,
                        transaction_observations = model.Observation,
                        category_id = model.CategoryId,
                        cost_center_id = model.CostCenterId
                    }
                },

                {
                    ExpenseSqlScripts.UPDATE_EXPENSE,
                    new
                    {
                        expense_id = model.ExpenseId,
                        supplier_id = model.SupplierId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
