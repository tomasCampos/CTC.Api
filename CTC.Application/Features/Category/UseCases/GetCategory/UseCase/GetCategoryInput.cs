using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Category.UseCases.GetCategory.UseCase
{
    public sealed class GetCategoryInput : IInput
    {
        public GetCategoryInput(string? categoryId)
        {
            this.categoryId = categoryId;
        }

        public string? categoryId { get; set; }
    }
}
