using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.Data
{
    internal interface IDeleteCostCenterRepository
    {
        Task<bool> DeleteCostCenter(string costCenterId, string addresId);

        Task<CostCenterModel> GetCostCenterById(string id);
    }
}
