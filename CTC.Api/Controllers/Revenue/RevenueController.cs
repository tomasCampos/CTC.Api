using CTC.Api.Controllers.Revenue.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Revenue.UseCases.RegisterRevenue.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.Revenue
{
    [ApiController]
    [Route("[controller]")]
    public sealed class RevenueController : BaseController
    {
        private readonly IUseCase<RegisterRevenueInput, Output> _registerRevenueUseCase;

        public RevenueController(IUseCase<RegisterRevenueInput, Output> registerRevenueUseCase)
        {
            _registerRevenueUseCase = registerRevenueUseCase;
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterRevenue([FromBody] RegisterRevenueRequest request)
        {
            var input = new RegisterRevenueInput
            {
                CategoryId = request.CategoryId,
                CostCenterId = request.CostCenterId,
                Observation = request.Observation,
                PaymentDate = request.PaymentDate,
                ClientId = request.ClientId,
                Value = request.Value,
            };

            var output = await _registerRevenueUseCase.Execute(input);
            return GetHttpResponse(output, "/revenue");
        }
    }
}
