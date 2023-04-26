using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.GetRevenue.Data
{
    internal interface IGetRevenueRepository
    {
        Task<RevenueModel> GetRevenue(string id);
    }
}
