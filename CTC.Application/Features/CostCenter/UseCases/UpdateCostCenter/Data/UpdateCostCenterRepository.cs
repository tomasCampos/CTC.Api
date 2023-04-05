using CTC.Application.Shared.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.Data
{
    internal sealed class UpdateCostCenterRepository : IUpdateCostCenterRepository
    {
        private readonly ISqlService _sqlService;

        public UpdateCostCenterRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<CostCenterModel> GetCostCenterById(string id)
        {
            var result = await _sqlService.SelectAsync<CostCenterModel>(CostCenterSqlScripts.SELECT_COST_CENTER_BY_ID, new { cost_center_id = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> VerifyIfCostCenterNameIsAlreadyUsed(string name)
        {
            var result = await _sqlService.CountAsync(CostCenterSqlScripts.COUNT_COST_CENTER_BY_NAME, new { cost_center_name = name });
            return result > 0;
        }

        public async Task<bool> UpdateCostCenter(CostCenterModel costCenter)
        {
            var commands = BuildCommands(costCenter);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        public async Task<bool> VerifyIfClientExists(string clientId)
        {
            var result = await _sqlService.CountAsync(CostCenterSqlScripts.COUNT_CLIENT_BY_ID, new { client_id = clientId });
            return result > 0;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(CostCenterModel model)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    CostCenterSqlScripts.UPDATE_COST_CENTER_ADDRESS,
                    new
                    {
                        address_id = model.AddressId,
                        address_postal_code = model.AddressPostalCode,
                        address_street = model.AddressStreetName,
                        address_neighborhood = model.AddressNeighborhood,
                        address_number = model.AddressNumber,
                        address_complement = model.AddressComplement,
                        address_city = model.AddressCity,
                        address_state = model.AddressStreetName
                    }
                },

                {
                    CostCenterSqlScripts.UPDATE_COST_CENTER,
                    new
                    {
                        cost_center_name = model.Name,
                        cost_center_observations = model.Observations,
                        cost_center_starting_date = model.StartingDate,
                        cost_center_closing_date = model.ClosingDate,
                        cost_center_closing_forecast_date = model.ExpectedClosingDate,
                        client_id = model.ClientId,
                        cost_center_id = model.Id
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
