using CTC.Api.Features.Category.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CTC.Api.Features.Category.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal sealed class CategoryController : ControllerBase
    {
        public CategoryController()
        {
        }

        [HttpPost()]
        public IActionResult Post(RegisterCategoryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}