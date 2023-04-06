using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.DeleteCategory.Data
{
    internal class DeleteCategoryRepository : IDeleteCategoryRepository
    {
        private readonly ISqlService _sqlService;

        public DeleteCategoryRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<CategoryModel> GetCategoryById(string? categoryId)
        {
            var result = await _sqlService.SelectAsync<CategoryModel>(CategorySqlScripts.GET_CATEGORY_BY_ID, new { category_id = categoryId });
            return result.FirstOrDefault();
        }

        public async Task<bool> DeleteCategory(string categoryId, string name)
        {
            var result = await _sqlService.ExecuteAsync(CategorySqlScripts.DELETE_CATEGORY, new { category_id = categoryId });

            return result > 0;
        }
    }
}
