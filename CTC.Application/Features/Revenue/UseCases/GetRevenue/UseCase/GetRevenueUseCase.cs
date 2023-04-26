using CTC.Application.Features.Revenue.UseCases.GetRevenue.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.GetRevenue.UseCase
{
    internal sealed class GetRevenueUseCase : IUseCase<GetRevenueInput, Output>
    {
        private readonly IGetRevenueRepository _revenueRepository;

        public GetRevenueUseCase(IGetRevenueRepository revenueRepository)
        {
            _revenueRepository = revenueRepository;
        }

        public async Task<Output> Execute(GetRevenueInput input)
        {
            var revenue = await _revenueRepository.GetRevenue(input.RevenueId!);

            if (revenue == null)
                return Output.CreateNotFoundResult();

            var result = new
            {
                id = revenue.RevenueId,
                transactionValue = revenue.Value,
                paymentDate = revenue.PaymentDate,
                observations = revenue.Observation,
                category = new
                {
                    id = revenue.CategoryId,
                    name = revenue.CategoryName
                },
                costCenter = new
                {
                    id = revenue.CostCenterId,
                    name = revenue.CostCenterName
                },
                client = new
                {
                    id = revenue.ClientId,
                    name = revenue.ClientName
                }
            };

            return Output.CreateOkResult(result);
        }
    }
}
