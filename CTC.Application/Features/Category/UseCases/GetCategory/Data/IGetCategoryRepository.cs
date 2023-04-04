namespace CTC.Application.Features.Category.UseCases.GetCategory.Data
{
    internal interface IGetCategoryRepository
    {
        CategoryModel GetCategory(string id);
    }
}
