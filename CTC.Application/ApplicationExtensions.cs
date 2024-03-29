﻿using CTC.Application.Features.Analytics;
using CTC.Application.Features.Category;
using CTC.Application.Features.Client;
using CTC.Application.Features.CostCenter;
using CTC.Application.Features.Expense;
using CTC.Application.Features.Revenue;
using CTC.Application.Features.Supplier;
using CTC.Application.Features.User;
using CTC.Application.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace CTC.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCategory();
            services.AddUser();
            services.AddSupplier();
            services.AddClient();
            services.AddCostCenter();
            services.AddShared();
            services.AddExpense();
            services.AddRevenue();
            services.AddAnalytics();
            return services;
        }
    }
}
