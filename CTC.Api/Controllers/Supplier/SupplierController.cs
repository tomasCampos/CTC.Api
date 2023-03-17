using CTC.Api.Controllers.Supplier.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
using CTC.Application.Features.Supplier.UseCases.UpdateSupplier.UseCase;
using CTC.Application.Features.User.UseCases.UpdateUser.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.Supplier
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SupplierController : BaseController
    {
        private readonly IUseCase<RegisterSupplierInput, Output> _registerSupplierUseCase;
        private readonly IUseCase<UpdateSupplierInput, Output> _updateSupplierUseCase;

        public SupplierController(IUseCase<RegisterSupplierInput, Output> registerSupplierUseCase, IUseCase<UpdateSupplierInput, Output> updateSupplierUseCase)
        {
            _registerSupplierUseCase = registerSupplierUseCase;
            _updateSupplierUseCase = updateSupplierUseCase;
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterSupplierRequest request)
        {
            var input = new RegisterSupplierInput(request.Name, request.Email, request.Phone, request.Document);

            var output = await _registerSupplierUseCase.Execute(input);
            return GetHttpResponse(output, "/user");
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierRequest request)
        {
            var input = new UpdateSupplierInput
            (
                request.Id,
                request.Name,
                request.Email,
                request.Phone,
                request.Document
            );

            var output = await _updateSupplierUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
