using CTC.Api.Controllers.CostCenter.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.UseCase;
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

        public CostCenterController
            (IUseCase<RegisterCostCenterInput, Output> registerCostCenterUseCase, 
            IUseCase<UpdateCostCenterInput, Output> updateCostCenterUseCase)
        {
            _registerCostCenterUseCase = registerCostCenterUseCase;
            _updateCostCenterUseCase = updateCostCenterUseCase;
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

            return GetHttpResponse(result);
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
                Id = request.id,
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
    }
}
