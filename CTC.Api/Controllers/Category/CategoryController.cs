using CTC.Api.Controllers.Category.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.Category
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CategoryController : BaseController
    {
        private readonly IUseCase<RegisterCategoryInput, Output> _registerCategoryUseCase;

        public CategoryController(IUseCase<RegisterCategoryInput, Output> registerCategoryUseCase)
        {
            _registerCategoryUseCase = registerCategoryUseCase;
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCategory([FromBody] RegisterCategoryRequest request)
        {
            var input = new RegisterCategoryInput(request.CategoryName);
            var output = await _registerCategoryUseCase.Execute(input);

            return GetHttpResponse(output, "/category");
        }
    }
}