using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Transaction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.UpdateRevenue.Data
{
    internal sealed class UpdateRevenueRepository : IUpdateRevenueRepository
    {
        private readonly ISqlService _sqlService;

        public UpdateRevenueRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<string> GetTransactionIdByRevenueId(string id)
        {
            var transactionId = await _sqlService.SelectAsync<string>(RevenueSqlScripts.SELECT_TRANSACTION_BY_REVENUE_ID, new { revenue_id = id });
            return transactionId.FirstOrDefault();
        }

        public async Task<bool> UpdateRevenue(RevenueModel revenue)
        {
            var commands = BuildCommands(revenue);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        public async Task<string> GetClientIdByCostCenterId(string costCenterId)
        {
            var clientId = await _sqlService.SelectAsync<string>(RevenueSqlScripts.SELECT_CLIENT_BY_COST_CENTER_ID, new { cost_center_id = costCenterId });
            return clientId.FirstOrDefault();
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(RevenueModel model)
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
                    RevenueSqlScripts.UPDATE_REVENUE,
                    new
                    {
                        revenue_id = model.RevenueId,
                        client_id = model.ClientId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
