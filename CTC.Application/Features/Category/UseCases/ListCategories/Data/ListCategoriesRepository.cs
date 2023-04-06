using CTC.Application.Features.Client.UseCases;
using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.ListCategories.Data
{
    internal sealed class ListCategoriesRepository : IListCategoriesRepository
    {
        private readonly ISqlService _sqlService;

        public ListCategoriesRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<PaginatedQueryResult<CategoryModel>> ListCategories(QueryRequest queryParams)
        {
            var result = await _sqlService.SelectPaginated<CategoryModel>(queryParams, CategorySqlScripts.LIST_CATEGORIES_SELECT_STATEMENT, CategorySqlScripts.LIST_CATEGORIES_FROM_AND_JOIN_STATEMENTS,
                                    CategorySqlScripts.LIST_CATEGORY_WHERE_STATEMENT);

            return result;
        }
    }
}
