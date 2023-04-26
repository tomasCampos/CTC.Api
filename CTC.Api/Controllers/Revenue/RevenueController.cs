using CTC.Api.Controllers.Revenue.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Revenue.UseCases.DeleteRevenue.UseCase;
using CTC.Application.Features.Revenue.UseCases.GetRevenue.UseCase;
using CTC.Application.Features.Revenue.UseCases.ListRevenues.UseCase;
using CTC.Application.Features.Revenue.UseCases.RegisterRevenue.UseCase;
using CTC.Application.Features.Revenue.UseCases.UpdateRevenue.UseCase;
using CTC.Application.Shared.Request;
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
        private readonly IUseCase<UpdateRevenueInput, Output> _updateRevenueUseCase;
        private readonly IUseCase<DeleteRevenueInput, Output> _deleteRevenueUseCase;
        private readonly IUseCase<ListRevenuesInput, Output> _listRevenuesUseCase;
        private readonly IUseCase<GetRevenueInput, Output> _getRevenueUseCase;

        public RevenueController(
            IUseCase<RegisterRevenueInput, Output> registerRevenueUseCase,
            IUseCase<UpdateRevenueInput, Output> updateRevenueUseCase,
            IUseCase<DeleteRevenueInput, Output> deleteRevenueUseCase,
            IUseCase<ListRevenuesInput, Output> listRevenuesUseCase,
            IUseCase<GetRevenueInput, Output> getRevenueUseCase)
        {
            _registerRevenueUseCase = registerRevenueUseCase;
            _updateRevenueUseCase = updateRevenueUseCase;
            _deleteRevenueUseCase = deleteRevenueUseCase;
            _listRevenuesUseCase = listRevenuesUseCase;
            _getRevenueUseCase = getRevenueUseCase;
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

        [Authorize]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateRevenue([FromBody] UpdateRevenueRequest request)
        {
            var input = new UpdateRevenueInput
            {
                RevenueId = request.RevenueId,
                CategoryId = request.CategoryId,
                CostCenterId = request.CostCenterId,
                Observation = request.Observation,
                PaymentDate = request.PaymentDate,
                ClientId = request.ClientId,
                Value = request.Value,
            };

            var output = await _updateRevenueUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpDelete("{revenueId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteRevenue([FromRoute] string revenueId)
        {
            var input = new DeleteRevenueInput { RevenueId = revenueId };
            var output = await _deleteRevenueUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListRevenue([FromQuery] int pageNumber, int pageSize, string? costCenterName, string? categoryName, int? year)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, null);
            var input = new ListRevenuesInput(request, costCenterName, categoryName, year);
            var output = await _listRevenuesUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet("{revenueId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListRevenue([FromRoute] string revenueId)
        {
            var input = new GetRevenueInput { RevenueId = revenueId };
            var result = await _getRevenueUseCase.Execute(input);

            return GetHttpResponse(result);
        }
    }
}
