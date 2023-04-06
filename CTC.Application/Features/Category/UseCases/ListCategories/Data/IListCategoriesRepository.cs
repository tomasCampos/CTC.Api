using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.ListCategories.Data
{
    internal interface IListCategoriesRepository
    {
        Task<PaginatedQueryResult<CategoryModel>> ListCategories(QueryRequest queryParams);
    }
}
