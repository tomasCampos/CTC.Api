using CTC.Api.Controllers.Supplier.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Supplier.UseCases.DeleteSupplier.UseCase;
using CTC.Application.Features.Supplier.UseCases.GetSupplier.UseCase;
using CTC.Application.Features.Supplier.UseCases.ListSuppliers.UseCase;
using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
using CTC.Application.Features.Supplier.UseCases.UpdateSupplier.UseCase;
using CTC.Application.Shared.Request;
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
        private readonly IUseCase<ListSuppliersInput, Output> _listSuppliersUseCase;
        private readonly IUseCase<UpdateSupplierInput, Output> _updateSupplierUseCase;
        private readonly IUseCase<GetSupplierInput, Output> _getSupplierUseCase;
        private readonly IUseCase<DeleteSupplierInput, Output> _deleteSupplierUseCase;

        public SupplierController(
            IUseCase<RegisterSupplierInput, Output> registerSupplierUseCase, 
            IUseCase<ListSuppliersInput, Output> listSuppliersUseCase, 
            IUseCase<UpdateSupplierInput, Output> updateSupplierUseCase,
            IUseCase<GetSupplierInput, Output> getSupplierUseCase,
            IUseCase<DeleteSupplierInput, Output> deleteSupplierUseCase)
        {
            _registerSupplierUseCase = registerSupplierUseCase;
            _listSuppliersUseCase = listSuppliersUseCase;
            _updateSupplierUseCase = updateSupplierUseCase;
            _getSupplierUseCase = getSupplierUseCase;
            _deleteSupplierUseCase = deleteSupplierUseCase;
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterSupplier([FromBody] RegisterSupplierRequest request)
        {
            var input = new RegisterSupplierInput(request.Name, request.Email, request.Phone, request.Document);

            var output = await _registerSupplierUseCase.Execute(input);
            return GetHttpResponse(output, "/supplier");
        }

        [Authorize]
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListSuppliers([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? queryParam)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, queryParam);
            var input = new ListSuppliersInput(request);
            var output = await _listSuppliersUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet("{supplierId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSupplier([FromRoute] string supplierId)
        {
            var input = new GetSupplierInput { SupplierId = supplierId };
            var output = await _getSupplierUseCase.Execute(input);
            return GetHttpResponse(output);
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

        [Authorize]
        [HttpDelete("{supplierId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteSupplier([FromRoute] string? supplierId)
        {
            var input = new DeleteSupplierInput { SupplierId = supplierId };
            var output = await _deleteSupplierUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
