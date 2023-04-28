﻿using CTC.Application.Features.Analytics.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.UseCases.GetOverview.UseCase
{
    internal sealed class GetOverviewUseCase : IUseCase<GetOverviewInput, Output>
    {
        private readonly ITransactionAnalyticsRepository _transactionAnalyticsRepository;

        public GetOverviewUseCase(ITransactionAnalyticsRepository transactionAnalyticsRepository)
        {
            _transactionAnalyticsRepository = transactionAnalyticsRepository;
        }

        public async Task<Output> Execute(GetOverviewInput input)
        {
            var expensesTask = _transactionAnalyticsRepository.ListExpensesByYear(input.Year, TransactionAnalyticsFiltersType.BeforeOrEqualsToYear);
            var revenueTask = _transactionAnalyticsRepository.ListRevenuesByYear(input.Year, TransactionAnalyticsFiltersType.BeforeOrEqualsToYear);
            await Task.WhenAll(expensesTask, revenueTask);
            var expensesData = expensesTask.Result;
            var revenuesData = revenueTask.Result;

            var currentYearExpenses = expensesData.Where(exp => exp.PaymentDate.Year == input.Year).Sum(exp => exp.TransactionValue);
            var currentYearRevenues = revenuesData.Where(rev => rev.PaymentDate.Year == input.Year).Sum(rev => rev.TransactionValue);
            var cashFlow = currentYearRevenues - currentYearExpenses;

            var totalExpenses = expensesData.Sum(exp => exp.TransactionValue);
            var totalRevenues = revenuesData.Sum(rev => rev.TransactionValue);
            var accumulatedBalance = totalRevenues - totalExpenses;

            var result = new 
            {
                currentYearRevenues,
                currentYearExpenses,
                cashFlow,
                accumulatedBalance
            };

            return Output.CreateOkResult(result);
        }
    }
}
