using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.RegisterCategory.Data
{
    internal interface IRegisterCategoryRepository
    {
        public Task InsertCategory(CategoryModel model);

        public Task<int> CountCategoryByName(string categoryName);
    }
}
