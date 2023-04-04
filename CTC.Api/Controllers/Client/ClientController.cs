using CTC.Api.Controllers.Client.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Client.UseCases.DeleteClient.UseCase;
using CTC.Application.Features.Client.UseCases.GetClient.UseCase;
using CTC.Application.Features.Client.UseCases.ListClients.UseCase;
using CTC.Application.Features.Client.UseCases.RegisterClient.UseCase;
using CTC.Application.Features.Client.UseCases.UpdateClient.UseCase;
using CTC.Application.Shared.Request;
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
        private readonly IUseCase<ListClientsInput, Output> _listClientsUseCase;
        private readonly IUseCase<GetClientInput, Output> _getClientUseCase;
        private readonly IUseCase<DeleteClientInput, Output> _deleteClientUseCase;
        private readonly IUseCase<RegisterClientInput, Output> _registerClientUseCase;
        private readonly IUseCase<UpdateClientInput, Output> _updateClientUseCase;

        public ClientController(
            IUseCase<ListClientsInput, Output> listClientsUseCase,
            IUseCase<GetClientInput, Output> getClientUseCase,
            IUseCase<DeleteClientInput, Output> deleteClientUseCase,
            IUseCase<RegisterClientInput, Output> registerClientUseCase,
            IUseCase<UpdateClientInput, Output> updateClientUseCase)
        {
            _listClientsUseCase = listClientsUseCase;
            _getClientUseCase = getClientUseCase;
            _deleteClientUseCase = deleteClientUseCase;
            _registerClientUseCase = registerClientUseCase;
            _updateClientUseCase = updateClientUseCase;
        }

        [Authorize]
        [HttpGet("{clientId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetClient([FromRoute] string clientId)
        {
            var input = new GetClientInput { ClientId = clientId };
            var output = await _getClientUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListClients([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? queryParam)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, queryParam);
            var input = new ListClientsInput(request);
            var output = await _listClientsUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpDelete("{clientId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteClient([FromRoute] string? clientId)
        {
            var input = new DeleteClientInput { ClientId = clientId };
            var output = await _deleteClientUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientRequest request)
        {
            var input = new RegisterClientInput(request.Name, request.Email, request.Phone, request.Document);

            var output = await _registerClientUseCase.Execute(input);
            return GetHttpResponse(output, "/client");
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientRequest request)
        {
            var input = new UpdateClientInput
            (
                request.Id,
                request.Name,
                request.Email,
                request.Phone,
                request.Document
            );

            var output = await _updateClientUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
