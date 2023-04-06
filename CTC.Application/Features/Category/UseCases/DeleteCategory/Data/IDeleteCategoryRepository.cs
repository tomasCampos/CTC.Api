using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.DeleteCategory.Data
{
    internal interface IDeleteCategoryRepository
    {
        Task<CategoryModel> GetCategoryById(string? categoryId);

        Task<bool> DeleteCategory(string categoryId, string name);
    }
}
