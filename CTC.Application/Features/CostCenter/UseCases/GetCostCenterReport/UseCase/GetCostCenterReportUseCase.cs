using CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.UseCase
{
    internal sealed class GetCostCenterReportUseCase : IUseCase<GetCostCenterReportInput, Output>
    {
        private readonly IGetCostCenterReportRepository _repository;

        public GetCostCenterReportUseCase(IGetCostCenterReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(GetCostCenterReportInput input)
        {
            var costCenter = await _repository.GetCostCenter(input.CostCenterId!);
            if (costCenter == null)
                return Output.CreateNotFoundResult();

            var costCenterTransactions = await _repository.ListTransactionsByCostCenterId(input.CostCenterId!);
            var result = new 
            {
                name = costCenter.Name,
                startingDate = costCenter.StartingDate,
                expectedClosingDate = costCenter.ExpectedClosingDate,
                closingDate = costCenter.ClosingDate,
                client = new 
                {
                    clientName = costCenter.ClientName,
                    clientDocument = costCenter.ClientDocument
                },
                address = new
                {
                    PostalCode = costCenter.AddressPostalCode,
                    StreetName = costCenter.AddressStreetName,
                    Neighborhood = costCenter.AddressNeighborhood,
                    Number = costCenter.AddressNumber,
                    Complement = costCenter.AddressComplement,
                    City = costCenter.AddressCity,
                    State = costCenter.AddressState
                },
                transactions = costCenterTransactions
            };

            return Output.CreateOkResult(result);
        }
    }
}
