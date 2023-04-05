using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Category.UseCases.UpdateCategory.UseCase
{
    public class UpdateCategoryInput : IInput
    {
        public  UpdateCategoryInput(in string? id, in string? name) 
        {
            Id = id;
            Name = name;
        }

        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
