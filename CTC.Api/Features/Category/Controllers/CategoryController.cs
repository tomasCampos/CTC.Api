using CTC.Api.Features.Category.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.Category.RegisterCategory.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Features.Category.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CategoryController : BaseController
    {
        private readonly IUseCase<RegisterCategoryInput, RegisterCategoryOutput> _registerCategoryUseCase;

        public CategoryController(IUseCase<RegisterCategoryInput, RegisterCategoryOutput> registerCategoryUseCase)
        {
            _registerCategoryUseCase = registerCategoryUseCase;
        }

        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] RegisterCategoryRequest request)
        {
            var input = new RegisterCategoryInput { CategoryName = request.CategoryName };
            var output = await _registerCategoryUseCase.Execute(input);

            return GetHttpresponse(output);
        }
    }
}