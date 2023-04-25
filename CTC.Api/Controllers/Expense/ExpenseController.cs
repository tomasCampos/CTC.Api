using CTC.Api.Controllers.Expense.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Expense.UseCases.DeleteExpense.UseCase;
using CTC.Application.Features.Expense.UseCases.ListExpenses.UseCase;
using CTC.Application.Features.Expense.UseCases.RegisterExpense.UseCase;
using CTC.Application.Features.Expense.UseCases.UpdateExpense.UseCase;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.Expense
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ExpenseController : BaseController
    {
        private readonly IUseCase<RegisterExpenseInput, Output> _registerExpenseUseCase;
        private readonly IUseCase<UpdateExpenseInput, Output> _updateExpenseUseCase;
        private readonly IUseCase<DeleteExpenseInput, Output> _deleteExpenseUseCase;
        private readonly IUseCase<ListExpensesInput, Output> _listExpensesUseCase;

        public ExpenseController(
            IUseCase<RegisterExpenseInput, Output> registerExpenseUseCase,
            IUseCase<UpdateExpenseInput, Output> updateExpenseUseCase,
            IUseCase<DeleteExpenseInput, Output> deleteExpenseUseCase,
            IUseCase<ListExpensesInput, Output> listExpensesUseCase)
        {
            _registerExpenseUseCase = registerExpenseUseCase;
            _updateExpenseUseCase = updateExpenseUseCase;
            _deleteExpenseUseCase = deleteExpenseUseCase;
            _listExpensesUseCase = listExpensesUseCase;
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterExpense([FromBody] RegisterExpenseRequest request)
        {
            var input = new RegisterExpenseInput 
            {
                CategoryId = request.CategoryId,
                CostCenterId = request.CostCenterId,
                Observation =  request.Observation,
                PaymentDate = request.PaymentDate,
                SupplierId = request.SupplierId,
                Value = request.Value,
            };

            var output = await _registerExpenseUseCase.Execute(input);
            return GetHttpResponse(output, "/expense");
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateExpense([FromBody] UpdateExpenseRequest request)
        {
            var input = new UpdateExpenseInput
            {
                ExpenseId = request.ExpenseId,
                CategoryId = request.CategoryId,
                CostCenterId = request.CostCenterId,
                Observation = request.Observation,
                PaymentDate = request.PaymentDate,
                SupplierId = request.SupplierId,
                Value = request.Value,
            };

            var output = await _updateExpenseUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpDelete("{expenseId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteExpense([FromRoute] string expenseId)
        {
            var input = new DeleteExpenseInput { ExpenseId = expenseId };
            var output = await _deleteExpenseUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteExpense([FromQuery] int pageNumber, int pageSize, string? costCenterName, string? categoryName, int? year)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, null);
            var input = new ListExpensesInput(request, costCenterName, categoryName, year);
            var output = await _listExpensesUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
