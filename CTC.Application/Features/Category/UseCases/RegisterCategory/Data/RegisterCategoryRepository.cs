using CTC.Application.Shared.Data;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.RegisterCategory.Data
{
    internal sealed class RegisterCategoryRepository : IRegisterCategoryRepository
    {
        private readonly ISqlService _sqlService;

        public RegisterCategoryRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<int> CountCategoryByName(string categoryName)
        {
            return await _sqlService.CountAsync(CategorySqlScripts.COUNT_CATEGORY_BY_NAME, new { category_name = categoryName });
        }

        public async Task InsertCategory(CategoryModel model)
        {
            _ = await _sqlService.ExecuteAsync(CategorySqlScripts.INSERT_CATEGORY, new { category_id = model.Id, category_name = model.Name });
        }
    }
}
