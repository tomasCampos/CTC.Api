using CTC.Api.Shared;
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

        public AnalyticsController(IUseCase<GetOverviewInput, Output> getOverviewUseCase)
        {
            _getOverviewUseCase = getOverviewUseCase;
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
    }
}
