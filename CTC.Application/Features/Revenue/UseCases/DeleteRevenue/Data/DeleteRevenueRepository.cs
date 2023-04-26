using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Transaction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.DeleteRevenue.Data
{
    internal sealed class DeleteRevenueRepository : IDeleteRevenueRepository
    {
        private readonly ISqlService _sqlService;

        public DeleteRevenueRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<string> GetTransactionIdByRevenueId(string id)
        {
            var transactionId = await _sqlService.SelectAsync<string>(RevenueSqlScripts.SELECT_TRANSACTION_BY_REVENUE_ID, new { revenue_id = id });
            return transactionId.FirstOrDefault();
        }

        public async Task<bool> DeleteRevenue(string revenueId, string transactionId)
        {
            var commands = BuildCommands(revenueId, transactionId);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(string revenueId, string transactionId)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    RevenueSqlScripts.DELETE_REVENUE,
                    new
                    {
                        revenue_id = revenueId
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
