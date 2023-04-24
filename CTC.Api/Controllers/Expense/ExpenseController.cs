using CTC.Api.Controllers.Expense.Contracts;
using CTC.Api.Controllers.Supplier.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Expense.UseCases.RegisterExpense.UseCase;
using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
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

        public ExpenseController(IUseCase<RegisterExpenseInput, Output> registerExpenseUseCase)
        {
            _registerExpenseUseCase = registerExpenseUseCase;
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
    }
}
