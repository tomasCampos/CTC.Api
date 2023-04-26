using CTC.Application.Features.Revenue.UseCases.ListRevenues.Data;
using CTC.Application.Shared.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.ListRevenues.UseCase
{
    internal class ListRevenuesUseCase : IUseCase<ListRevenuesInput, Output>
    {
        private readonly IListRevenuesRepository _repository;

        public ListRevenuesUseCase(IListRevenuesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(ListRevenuesInput input)
        {
            var revenues = await _repository.ListRevenues(input.Request, input.CostCenterName, input.CategoryName, input.Year);
            var result = FormatExpenseData(revenues);

            return Output.CreateOkResult(result);
        }

        private static PaginatedQueryResult<object> FormatExpenseData(PaginatedQueryResult<RevenueModel> data)
        {
            var formatedRevenueList = new List<object>();
            foreach (var revenue in data.Results)
            {
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

                formatedRevenueList.Add(result);
            }

            return new PaginatedQueryResult<object>(formatedRevenueList, data.TotalCount);
        }
    }
}
