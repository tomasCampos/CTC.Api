using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.Data
{
    internal interface IUpdateCostCenterRepository
    {
        Task<CostCenterModel> GetCostCenterById(string id);

        Task<bool> VerifyIfCostCenterNameIsAlreadyUsed(string name);

        Task<bool> VerifyIfClientExists(string clientId);

        Task<bool> UpdateCostCenter(CostCenterModel costCenter);
    }
}
