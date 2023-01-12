using System;

namespace CTC.Application.Features.Category
{
    internal sealed class CategoryModel
    {
        public CategoryModel(in string categoryName)
        {
            if(string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentNullException(nameof(categoryName));

            CategoryName = categoryName;
            CategoryId = Guid.NewGuid().ToString();
        }

        public CategoryModel(in string categoryName, in string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentNullException(nameof(categoryName));
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new ArgumentNullException(nameof(categoryId));

            CategoryName = categoryName;
            CategoryId = categoryId;
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
