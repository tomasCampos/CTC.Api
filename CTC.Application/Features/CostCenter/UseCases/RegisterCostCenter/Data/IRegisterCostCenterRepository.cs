using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.Data
{
    internal interface IRegisterCostCenterRepository
    {
        Task<bool> VerifyIfCostCenterAlreadyExists(string name);

        Task<bool> InsertCostCenter(CostCenterModel costCenter);

        Task<bool> VerifyIfCostCenterClientExists(string clientId);
    }
}
