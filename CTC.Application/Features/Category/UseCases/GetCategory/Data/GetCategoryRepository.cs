using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.GetCategory.Data
{
    internal class GetCategoryRepository : IGetCategoryRepository
    {
        private readonly ISqlService _sqlService;

        public GetCategoryRepository(ISqlService sqlService) 
        {
            _sqlService = sqlService;
        }

        public async Task<CategoryModel> GetCategoryById(string categoryId)
        {
            var result = await _sqlService.SelectAsync<CategoryModel>(CategorySqlScripts.GET_CATEGORY_BY_ID, new { category_id = categoryId });
            return result.FirstOrDefault();
        }
    }
}
