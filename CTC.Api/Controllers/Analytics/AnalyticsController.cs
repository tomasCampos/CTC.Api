using CTC.Api.Shared;
using CTC.Application.Features.Analytics.UseCases.GetCashFlow.UseCase;
using CTC.Application.Features.Analytics.UseCases.GetOverview.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.Analytics
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AnalyticsController : BaseController
    {
        private readonly IUseCase<GetOverviewInput, Output> _getOverviewUseCase;
        private readonly IUseCase<GetCashFlowInput, Output> _getCashFlowUseCase;

        public AnalyticsController(IUseCase<GetOverviewInput, Output> getOverviewUseCase, IUseCase<GetCashFlowInput, Output> getCashFlowUseCase)
        {
            _getOverviewUseCase = getOverviewUseCase;
            _getCashFlowUseCase = getCashFlowUseCase;
        }

        [Authorize]
        [HttpGet("Overview/{year}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCategory([FromRoute] int year)
        {
            var input = new GetOverviewInput { Year = year };
            var output = await _getOverviewUseCase.Execute(input);

            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet("CashFlow/{year}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCashFlow([FromRoute] int year)
        {
            var input = new GetCashFlowInput { Year = year };
            var output = await _getCashFlowUseCase.Execute(input);

            return GetHttpResponse(output);
        }
    }
}
