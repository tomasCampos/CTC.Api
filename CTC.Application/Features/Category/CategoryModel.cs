using System;

namespace CTC.Application.Features.Category
{
    internal sealed class CategoryModel
    {
        public CategoryModel(string categoryName)
        {
            if(string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentNullException(nameof(categoryName));

            CategoryName = categoryName;
            CategoryId = Guid.NewGuid().ToString();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
