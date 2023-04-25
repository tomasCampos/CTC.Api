using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.RegisterRevenue.Data
{
    internal sealed class RegisterRevenueRepository : IRegisterRevenueRepository
    {
        private readonly ISqlService _sqlService;

        public RegisterRevenueRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> InsertRevenue(RevenueModel model)
        {
            var commands = BuildCommands(model);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(RevenueModel model)
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
                    RevenueSqlScripts.INSERT_REVENUE,
                    new
                    {
                        revenue_id = model.RevenueId,
                        client_id = model.ClientId,
                        transaction_id = model.TransactionId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
