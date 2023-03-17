﻿using CTC.Api.Controllers.Supplier.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Supplier.UseCases.ListSuppliers.UseCase;
using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
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
        private readonly IUseCase<ListSuppliersUseCaseInput, Output> _listSuppliersUseCase;

        public SupplierController(IUseCase<RegisterSupplierInput, Output> registerSupplierUseCase,
                                  IUseCase<ListSuppliersUseCaseInput, Output> listSuppliersUseCase)
        {
            _registerSupplierUseCase = registerSupplierUseCase;
            _listSuppliersUseCase = listSuppliersUseCase;
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
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListSuppliers([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? queryParam)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, queryParam);
            var input = new ListSuppliersUseCaseInput(request);
            var output = await _listSuppliersUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
