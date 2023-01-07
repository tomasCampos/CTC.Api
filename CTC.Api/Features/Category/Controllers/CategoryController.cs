using CTC.Api.Features.Category.Contracts;
using CTC.Application.Features.Category.RegisterCategory.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace CTC.Api.Features.Category.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal sealed class CategoryController : ControllerBase
    {
        private readonly IUseCase<RegisterCategoryInput, RegisterCategoryOutput> _registerCategoryUseCase;

        public CategoryController(IUseCase<RegisterCategoryInput, RegisterCategoryOutput> registerCategoryUseCase)
        {
            _registerCategoryUseCase = registerCategoryUseCase;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(RegisterCategoryRequest request)
        {
            var input = new RegisterCategoryInput { CategoryName = request.CategoryName };
            var output = await _registerCategoryUseCase.Execute(input);

            if(output.Success)
            {
                //Todo: O Success nao faz sentido. Pode representar mais do que so erros de validação. melhor pegar direto o httpStatus
            }
        }
    }
}