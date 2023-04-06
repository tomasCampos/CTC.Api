using CTC.Application.Shared.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.Data
{
    internal sealed class DeleteCostCenterRepository : IDeleteCostCenterRepository
    {
        private readonly ISqlService _sqlService;

        public DeleteCostCenterRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> DeleteCostCenter(string costCenterId, string addresId)
        {
            var commands = BuildCommands(costCenterId, addresId);
            var result = await _sqlService.ExecuteWithTransactionAsync(commands);

            return result.Success;
        }

        public async Task<CostCenterModel> GetCostCenterById(string id)
        {
            var result = await _sqlService.SelectAsync<CostCenterModel>(CostCenterSqlScripts.SELECT_COST_CENTER_BY_ID, new { cost_center_id = id });
            return result.FirstOrDefault();
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(string costCenterId, string addresId)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    CostCenterSqlScripts.DELETE_COST_CENTER_ADDRESS,
                    new
                    {
                        addres_id = addresId
                    }
                },

                {
                    CostCenterSqlScripts.DELETE_COST_CENTER,
                    new
                    {
                        cost_center_id = costCenterId
                    }
                }
            };

            return commands;
        }

        #endregion
    }
}
