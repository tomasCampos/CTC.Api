using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Category.UseCases.DeleteCategory.UseCase
{
    public sealed class DeleteCategoryInput : IInput
    {
        public string? CategoryId { get; set; }
    }
}
