using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Category.UseCases.ListCategories.UseCase
{
    public sealed class ListCategoriesInput : QueryInput
    {
        public ListCategoriesInput(QueryRequest request) : base(request) { }
    }
}
