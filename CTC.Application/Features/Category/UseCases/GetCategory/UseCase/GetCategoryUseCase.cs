using CTC.Application.Features.Category.UseCases.GetCategory.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.GetCategory.UseCase
{
    internal sealed class GetCategoryUseCase : IUseCase<GetCategoryInput, Output>
    {
        private readonly IGetCategoryRepository _getCategoryRepository;

        public GetCategoryUseCase(IGetCategoryRepository getCategoryRepository)
        {
            _getCategoryRepository = getCategoryRepository;
        }

        public Task<Output> Execute(GetCategoryInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
