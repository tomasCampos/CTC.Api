using CTC.Application.Shared.Data;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.RegisterCategory.Repositories
{
    internal sealed class RegisterCategoryRepository : IRegisterCategoryRepository
    {
        private readonly IMySqlDataBaseConnector _mySqlDataBaseConnector;

        public RegisterCategoryRepository(IMySqlDataBaseConnector mySqlDataBaseConnector)
        {
            _mySqlDataBaseConnector = mySqlDataBaseConnector;
        }

        public async Task<int> CountCategoryByName(string categoryName)
        {
            return await _mySqlDataBaseConnector.CountAsync(RegisterCategorySqlScripts.COUNT_CATEGORY_BY_NAME_SQL, new { category_name = categoryName });
        }

        public async Task InsertCategory(CategoryModel model)
        {
            _ = await _mySqlDataBaseConnector.ExecuteAsync(RegisterCategorySqlScripts.INSERT_CATEGORY_SQL, new { category_id = model.CategoryId, category_name = model.CategoryName});
        }
    }
}
