using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.RegisterRevenue.Data
{
    internal interface IRegisterRevenueRepository
    {
        Task<bool> InsertRevenue(RevenueModel model);

        Task<string> GetClientIdByCostCenterId(string costCenterId);
    }
}
