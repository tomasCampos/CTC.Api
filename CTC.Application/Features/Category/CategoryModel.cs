using System;

namespace CTC.Application.Features.Category
{
    internal sealed class CategoryModel
    {
        public CategoryModel(in string categoryName)
        {
            Name = categoryName;
            Id = Guid.NewGuid().ToString();
        }

        public CategoryModel(in string categoryName, in string categoryId)
        {
            Name = categoryName;
            Id = categoryId;
        }

        public CategoryModel() { }

        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
