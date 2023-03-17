using CTC.Api.Shared;
using CTC.Application.Features.Client.UseCases.GetClient.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.Client
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ClientController : BaseController
    {
        private readonly IUseCase<GetClientInput, Output> _getClientUseCase;

        public ClientController(
            IUseCase<GetClientInput, Output> getClientUseCase)
        {
            _getClientUseCase = getClientUseCase;
        }

        [Authorize]
        [HttpGet("{clientId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetClient([FromRoute] string clientId)
        {
            var input = new GetClientInput { ClientId = clientId };
            var output = await _getClientUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
