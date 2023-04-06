using CTC.Application.Features.Category.UseCases.ListCategories.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.ListCategories.UseCase
{
    internal sealed class ListCategoriesUseCase : IUseCase<ListCategoriesInput, Output>
    {
        private readonly IListCategoriesRepository _categoriesRepository;

        public ListCategoriesUseCase(IListCategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<Output> Execute(ListCategoriesInput input)
        {
            var result = await _categoriesRepository.ListCategories(input.Request);
            return Output.CreateOkResult(result);
        }
    }
}
