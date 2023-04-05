using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.UpdateCategory.Data
{
    internal interface IUpdateCategoryRepository
    {
        Task<CategoryModel> GetCategoryById(string id);

        Task<int> UpdateCategory(CategoryModel model);
    }
}
