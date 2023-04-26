using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Revenue.UseCases.ListRevenues.UseCase
{
    public sealed class ListRevenuesInput : QueryInput
    {
        public ListRevenuesInput(QueryRequest request, in string? costCenterName, in string? categoryName, in int? year) : base(request)
        {
            CostCenterName = costCenterName;
            CategoryName = categoryName;
            Year = year;
        }

        public string? CostCenterName { get; set; }
        public string? CategoryName { get; set; }
        public int? Year { get; set; }
    }
}
