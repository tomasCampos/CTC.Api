using CTC.Api.Controllers.Category.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Category.UseCases.GetCategory.UseCase;
using CTC.Application.Features.Category.UseCases.ListCategories.UseCase;
using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase;
using CTC.Application.Features.Client.UseCases.ListClients.UseCase;
using CTC.Application.Shared.Request;
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
        private readonly IUseCase<GetCategoryInput, Output> _getCategoryUseCase;
        private readonly IUseCase<ListCategoriesInput, Output> _listCategoriesUseCase;

        public CategoryController(IUseCase<RegisterCategoryInput, Output> registerCategoryUseCase, 
                                  IUseCase<GetCategoryInput, Output> getCategoryUseCase,
                                  IUseCase<ListCategoriesInput, Output> listCategoriesUseCase)
        {
            _registerCategoryUseCase = registerCategoryUseCase;
            _getCategoryUseCase = getCategoryUseCase;
            _listCategoriesUseCase = listCategoriesUseCase;
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

        [Authorize]
        [HttpGet("{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCategory([FromRoute] string categoryId)
        {
            var input = new GetCategoryInput(categoryId);
            var output = await _getCategoryUseCase.Execute(input);

            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListCategories([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? queryParam)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, queryParam);
            var input = new ListCategoriesInput(request);
            var output = await _listCategoriesUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}