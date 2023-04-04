using CTC.Application.Shared.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.Data
{
    internal sealed class RegisterCostCenterRepository : IRegisterCostCenterRepository
    {
        private readonly ISqlService _sqlService;

        public RegisterCostCenterRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> InsertCostCenter(CostCenterModel costCenter)
        {
            var commands = BuildCommands(costCenter);
            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        public async Task<bool> VerifyIfCostCenterAlreadyExists(string name)
        {
            var countResult = await _sqlService.CountAsync(CostCenterSqlScripts.COUNT_COST_CENTER_BY_NAME, new { cost_center_name = name });
            return countResult > 0;
        }

        public async Task<bool> VerifyIfCostCenterClientExists(string clientId)
        {
            var countResult = await _sqlService.CountAsync(CostCenterSqlScripts.COUNT_CLIENT_BY_ID, new { client_id = clientId });
            return countResult > 0;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(CostCenterModel model)
        {
            var addresId = Guid.NewGuid().ToString();
            var commands = new Dictionary<string, object?>
            {
                {
                    CostCenterSqlScripts.INSERT_COST_CENTER_ADDRESS,
                    new
                    {
                        address_id = addresId,
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
                    CostCenterSqlScripts.INSERT_COST_CENTER,
                    new
                    {
                        cost_center_id = model.Id,
                        cost_center_name = model.Name,
                        cost_center_observations = model.Observations,
                        cost_center_starting_date = model.StartingDate,
                        cost_center_closing_forecast_date = model.ClosingDate,
                        cost_center_closing_date = model.ClosingDate,
                        address_id = addresId,
                        client_id = model.ClientId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
