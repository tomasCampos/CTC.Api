using System.Threading.Tasks;

namespace CTC.Application.Features.Category.RegisterCategory.Repositories
{
    internal sealed class RegisterCategoryRepository : IRegisterCategoryRepository
    {
        public Task<int> SearchCategoryByName(string categoryName)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertCategory(CategoryModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
