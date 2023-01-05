using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Category.RegisterCategory.UseCase.IO
{
    internal sealed class RegisterCategoryInput : IInput
    {
        public string? CategoryName { get; set; }
    }
}
