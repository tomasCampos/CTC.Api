using CTC.Api.Controllers.Category.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Category.UseCases.DeleteCategory.UseCase;
using CTC.Application.Features.Category.UseCases.GetCategory.UseCase;
using CTC.Application.Features.Category.UseCases.ListCategories.UseCase;
using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase;
using CTC.Application.Features.Category.UseCases.UpdateCategory.UseCase;
using CTC.Application.Features.Supplier.UseCases.DeleteSupplier.UseCase;
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
        private readonly IUseCase<UpdateCategoryInput, Output> _updateCategoryUseCase;
        private readonly IUseCase<DeleteCategoryInput, Output> _deleteCategoryUseCase;

        public CategoryController(IUseCase<RegisterCategoryInput, Output> registerCategoryUseCase, 
                                  IUseCase<GetCategoryInput, Output> getCategoryUseCase,
                                  IUseCase<ListCategoriesInput, Output> listCategoriesUseCase,
                                  IUseCase<UpdateCategoryInput, Output> updateCategoryUseCase,
                                  IUseCase<DeleteCategoryInput, Output> deleteCategoryUseCase)
        {
            _registerCategoryUseCase = registerCategoryUseCase;
            _getCategoryUseCase = getCategoryUseCase;
            _listCategoriesUseCase = listCategoriesUseCase;
            _updateCategoryUseCase = updateCategoryUseCase;
            _deleteCategoryUseCase = deleteCategoryUseCase;
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

        [Authorize]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            var input = new UpdateCategoryInput
            (
                request.Id,
                request.Name
            );

            var output = await _updateCategoryUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpDelete("{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteCategory([FromRoute] string? categoryId)
        {
            var input = new DeleteCategoryInput { CategoryId = categoryId };
            var output = await _deleteCategoryUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}