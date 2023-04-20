using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.RegisterExpense.Data
{
    internal sealed class RegisterExpenseRepository : IRegisterExpenseRepository
    {
        private readonly ISqlService _sqlService;

        public RegisterExpenseRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> InsertExpense(ExpenseModel model)
        {
            var commands = BuildCommands(model);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(ExpenseModel model)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    TransactionSqlScripts.INSERT_TRANSACTION,
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
                    ExpenseSqlScripts.INSERT_EXPENSE,
                    new
                    {
                        expense_id = model.ExpenseId,
                        supplier_id = model.SupplierId,
                        transaction_id = model.TransactionId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
