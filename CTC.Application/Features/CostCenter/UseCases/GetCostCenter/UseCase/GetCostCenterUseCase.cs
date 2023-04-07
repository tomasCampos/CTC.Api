using CTC.Application.Features.CostCenter.UseCases.GetCostCenter.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenter.UseCase
{
    internal sealed class GetCostCenterUseCase : IUseCase<GetCostCenterInput, Output>
    {
        private readonly IGetCostCenterRepository _repository;

        public GetCostCenterUseCase(IGetCostCenterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(GetCostCenterInput input)
        {
            var costCenter = await _repository.GetCostcenter(input.Id);

            if(costCenter == null)
                return Output.CreateNotFoundResult();

            var result = new 
            {
                costCenter.Id,
                costCenter.Name,
                costCenter.Observations,
                costCenter.StartingDate,
                costCenter.ExpectedClosingDate,
                costCenter.ClosingDate,
                Client = new 
                {
                    Id = costCenter.ClientId,
                    costCenter.ClientName
                },
                Address = new 
                {
                    Id = costCenter.AddressId,
                    PostalCode = costCenter.AddressPostalCode,
                    StreetName = costCenter.AddressStreetName,
                    Neighborhood = costCenter.AddressNeighborhood,
                    Number = costCenter.AddressNumber,
                    Complement = costCenter.AddressComplement,
                    City = costCenter.AddressCity,
                    State = costCenter.AddressState
                }
            };

            return Output.CreateOkResult(result);
        }
    }
}
