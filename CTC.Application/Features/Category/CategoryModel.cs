using System;

namespace CTC.Application.Features.Category
{
    internal sealed class CategoryModel
    {
        public CategoryModel(in string categoryName)
        {
            if(string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentNullException(nameof(categoryName));

            Name = categoryName;
            Id = Guid.NewGuid().ToString();
        }

        public CategoryModel(in string categoryName, in string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentNullException(nameof(categoryName));
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new ArgumentNullException(nameof(categoryId));

            Name = categoryName;
            Id = categoryId;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        //TODO: Refatorar para remover a palavra Category do nome das propriedades
    }
}
