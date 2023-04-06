using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenter.Data
{
    internal interface IGetCostCenterRepository
    {
        Task<CostCenterModel> GetCostcenter(string id);
    }
}
