using CTC.Application.Features.Analytics.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.UseCases.GetCashFlow.UseCase
{
    internal sealed class GetCashFlowUseCase : IUseCase<GetCashFlowInput, Output>
    {
        private readonly ITransactionAnalyticsRepository _transactionAnalyticsRepository;

        public GetCashFlowUseCase(ITransactionAnalyticsRepository transactionAnalyticsRepository)
        {
            _transactionAnalyticsRepository = transactionAnalyticsRepository;
        }

        public async Task<Output> Execute(GetCashFlowInput input)
        {
            (var expensesData, var revenuesData) = await _transactionAnalyticsRepository.ListTransactionsByYear(input.Year, TransactionAnalyticsFiltersType.BeforeOrEqualsToYear);

            var january = GetMonthCashFlow(1, expensesData, revenuesData);
            var february = GetMonthCashFlow(2, expensesData, revenuesData);
            var march = GetMonthCashFlow(3, expensesData, revenuesData);
            var april = GetMonthCashFlow(4, expensesData, revenuesData);
            var may = GetMonthCashFlow(5, expensesData, revenuesData);
            var june = GetMonthCashFlow(6, expensesData, revenuesData);
            var july = GetMonthCashFlow(7, expensesData, revenuesData);
            var august = GetMonthCashFlow(8, expensesData, revenuesData);
            var september = GetMonthCashFlow(9, expensesData, revenuesData);
            var october = GetMonthCashFlow(10, expensesData, revenuesData);
            var november = GetMonthCashFlow(11, expensesData, revenuesData);
            var december = GetMonthCashFlow(12, expensesData, revenuesData);
            await Task.WhenAll(january, february, march, april, may, june, july, august, september, october, november, december);

            return Output.CreateOkResult(new
            {
                january = january.Result,
                february = february.Result,
                march = march.Result,
                april = april.Result,
                may = may.Result,
                june = june.Result,
                july = july.Result,
                august = august.Result,
                september = september.Result,
                october = october.Result,
                november = november.Result,
                december = december.Result,
            });
        }

        private Task<decimal> GetMonthCashFlow(int month, IEnumerable<TransactionAnalyticsModel> expensesData, IEnumerable<TransactionAnalyticsModel> revenueData)
        {
            var expense = expensesData.AsParallel().Where(exp => exp.PaymentDate.Month == month).Sum(exp => exp.TransactionValue);
            var revenue = revenueData.AsParallel().Where(exp => exp.PaymentDate.Month == month).Sum(exp => exp.TransactionValue);
            var result = revenue - expense;

            return Task.FromResult(result);
        }
    }
}
