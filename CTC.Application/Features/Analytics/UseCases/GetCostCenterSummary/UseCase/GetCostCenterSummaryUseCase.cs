using CTC.Application.Features.Analytics.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.UseCases.GetCostCenterSummary.UseCase
{
    internal sealed class GetCostCenterSummaryUseCase : IUseCase<GetCostCenterSummaryInput, Output>
    {
        private readonly ITransactionAnalyticsRepository _transactionAnalyticsRepository;

        public GetCostCenterSummaryUseCase(ITransactionAnalyticsRepository transactionAnalyticsRepository)
        {
            _transactionAnalyticsRepository = transactionAnalyticsRepository;
        }

        public async Task<Output> Execute(GetCostCenterSummaryInput input)
        {
            (var expensesData, var revenuesData) = await _transactionAnalyticsRepository.ListTransactionsByYear(input.Year, TransactionAnalyticsFiltersType.EqualsToYear);            
            var distictCostCenter = expensesData.Concat(revenuesData).Select(t => t.CostCenterId).Distinct();

            var summary = new Dictionary<string, decimal>();
            foreach (var costCenter in distictCostCenter)
            {
                var costCenterName = expensesData.FirstOrDefault(tran => tran.CostCenterId == costCenter).CostCenterName;

                var expenses = expensesData.AsParallel().Where(exp => exp.CostCenterId == costCenter).Sum(exp => exp.TransactionValue);
                var revenues = revenuesData.AsParallel().Where(rev => rev.CostCenterId == costCenter).Sum(rev => rev.TransactionValue);
                var costCenterCashFlow = revenues - expenses;

                summary.Add(costCenterName!, costCenterCashFlow);
            }

            return Output.CreateOkResult(summary);
        }
    }
}
