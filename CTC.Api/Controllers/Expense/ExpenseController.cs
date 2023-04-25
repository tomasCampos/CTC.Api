using CTC.Api.Controllers.Expense.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Expense.UseCases.RegisterExpense.UseCase;
using CTC.Application.Features.Expense.UseCases.UpdateExpense.UseCase;
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

        public ExpenseController(IUseCase<RegisterExpenseInput, Output> registerExpenseUseCase, IUseCase<UpdateExpenseInput, Output> updateExpenseUseCase)
        {
            _registerExpenseUseCase = registerExpenseUseCase;
            _updateExpenseUseCase = updateExpenseUseCase;
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
        public async Task<IActionResult> RegisterExpense([FromBody] UpdateExpenseRequest request)
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
    }
}
