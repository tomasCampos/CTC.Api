using CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.Data
{
    internal interface IGetCostCenterReportRepository
    {
        Task<CostCenterModel> GetCostCenter(string id);

        Task<IEnumerable<CostCenterTransactionsModel>> ListTransactionsByCostCenterId(string id);
    }
}