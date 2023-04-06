using CTC.Api.Controllers.CostCenter.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.GetCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.ListCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.UseCase;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.CostCenter
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CostCenterController : BaseController
    {
        private readonly IUseCase<RegisterCostCenterInput, Output> _registerCostCenterUseCase;
        private readonly IUseCase<UpdateCostCenterInput, Output> _updateCostCenterUseCase;
        private readonly IUseCase<GetCostCenterInput, Output> _getCostCenterUseCase;
        private readonly IUseCase<DeleteCostCenterInput, Output> _deleteCostCenterUseCase;
        private readonly IUseCase<ListCostCentersInput, Output> _listCostCentersUseCase;

        public CostCenterController
            (IUseCase<RegisterCostCenterInput, Output> registerCostCenterUseCase, 
            IUseCase<UpdateCostCenterInput, Output> updateCostCenterUseCase,
            IUseCase<GetCostCenterInput, Output> getCostCenterUseCase,
            IUseCase<DeleteCostCenterInput, Output> deleteCostCenterUseCase,
            IUseCase<ListCostCentersInput, Output> listCostCentersUseCase)
        {
            _registerCostCenterUseCase = registerCostCenterUseCase;
            _updateCostCenterUseCase = updateCostCenterUseCase;
            _getCostCenterUseCase = getCostCenterUseCase;
            _deleteCostCenterUseCase = deleteCostCenterUseCase;
            _listCostCentersUseCase = listCostCentersUseCase;
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCostCenter([FromBody] RegisterCostCenterRequest request)
        {
            var input = new RegisterCostCenterInput
            {
                AddressCity = request.Address?.City,
                AddressComplement = request.Address?.Complement,
                AddressNeighborhood = request.Address?.Neighborhood,
                AddressNumber = request.Address?.Number,
                AddressPostalCode = request.Address?.PostalCode,
                AddressState = request.Address?.State,
                AddressStreetName = request.Address?.StreetName,
                ClientId = request.ClientId,
                ClosingDate = request.ClosingDate,
                ExpectedClosingDate = request.ExpectedClosingDate,
                Name = request.Name,
                Observations = request.Observations,
                StartingDate = request.StartingDate
            };

            var result = await _registerCostCenterUseCase.Execute(input);

            return GetHttpResponse(result, "/costCenter");
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateCostCenter([FromBody] UpdateCostCenterRequest request)
        {
            var input = new UpdateCostCenterInput
            {
                Id = request.Id,
                AddressCity = request.Address?.City,
                AddressComplement = request.Address?.Complement,
                AddressNeighborhood = request.Address?.Neighborhood,
                AddressNumber = request.Address?.Number,
                AddressPostalCode = request.Address?.PostalCode,
                AddressState = request.Address?.State,
                AddressStreetName = request.Address?.StreetName,
                ClientId = request.ClientId,
                ClosingDate = request.ClosingDate,
                ExpectedClosingDate = request.ExpectedClosingDate,
                Name = request.Name,
                Observations = request.Observations,
                StartingDate = request.StartingDate
            };

            var result = await _updateCostCenterUseCase.Execute(input);

            return GetHttpResponse(result);
        }

        [Authorize]
        [HttpGet("{costCenterId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCostCenter([FromRoute] string costCenterId)
        {
            var input = new GetCostCenterInput(costCenterId);
            var result = await _getCostCenterUseCase.Execute(input);

            return GetHttpResponse(result);
        }

        [Authorize]
        [HttpDelete("{costCenterId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteCostCenter([FromRoute] string costCenterId)
        {
            var input = new DeleteCostCenterInput(costCenterId);
            var result = await _deleteCostCenterUseCase.Execute(input);

            return GetHttpResponse(result);
        }

        [Authorize]
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListCostCenters([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? queryParam)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, queryParam);
            var input = new ListCostCentersInput(request);
            var output = await _listCostCentersUseCase.Execute(input);

            return GetHttpResponse(output);
        }
    }
}
