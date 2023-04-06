using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.GetCategory.Data
{
    internal interface IGetCategoryRepository
    {
        Task<CategoryModel> GetCategoryById(string categoryId);
    }
}
