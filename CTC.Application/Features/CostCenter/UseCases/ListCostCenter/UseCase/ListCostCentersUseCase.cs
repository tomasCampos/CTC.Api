using CTC.Application.Features.CostCenter.UseCases.ListCostCenter.Data;
using CTC.Application.Shared.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.ListCostCenter.UseCase
{
    internal sealed class ListCostCentersUseCase : IUseCase<ListCostCentersInput, Output>
    {
        private readonly IListCostCentersRepository _repository;

        public ListCostCentersUseCase(IListCostCentersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(ListCostCentersInput input)
        {
            var result = await _repository.ListCostCenters(input.Request);
            var formatedCostCenterList = FormatCostCenterData(result);

            return Output.CreateOkResult(formatedCostCenterList);
        }

        private static PaginatedQueryResult<object> FormatCostCenterData(PaginatedQueryResult<CostCenterModel> result)
        {
            var formatedCostCenterList = new List<object>();
            foreach (var item in result.Results)
            {
                var costCenter = new
                {
                    item.Id,
                    item.Name,
                    item.Observations,
                    item.StartingDate,
                    item.ExpectedClosingDate,
                    item.ClosingDate,
                    Client = new
                    {
                        Id = item.ClientId,
                        item.ClientName,
                        item.ClientDocument
                    },
                    Address = new
                    {
                        Id = item.AddressId,
                        PostalCode = item.AddressPostalCode,
                        StreetName = item.AddressStreetName,
                        Neighborhood = item.AddressNeighborhood,
                        Number = item.AddressNumber,
                        Complement = item.AddressComplement,
                        City = item.AddressCity,
                        State = item.AddressState
                    }
                };

                formatedCostCenterList.Add(costCenter);
            }

            return new PaginatedQueryResult<object>(formatedCostCenterList, result.TotalCount);
        }
    }
}
