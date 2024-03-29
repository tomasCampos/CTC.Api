﻿using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.UpdateCategory.Data
{
    internal sealed class UpdateCategoryRepository : IUpdateCategoryRepository
    {
        private readonly ISqlService _sqlService;

        public UpdateCategoryRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<CategoryModel> GetCategoryById(string id)
        {
            var category = await _sqlService.SelectAsync<CategoryModel>(CategorySqlScripts.GET_CATEGORY_BY_ID, new { category_id = id });
            return category.FirstOrDefault();
        }

        public async Task<int> CountCategoryByName(string name)
        {
            var count = await _sqlService.CountAsync(CategorySqlScripts.COUNT_CATEGORIES_BY_NAME, new { category_name = name });

            return count;
        }

        public async Task<int> UpdateCategory(CategoryModel model)
        {
            var result = await _sqlService.ExecuteAsync(CategorySqlScripts.UPDATE_CATEGORY, new
            {
                category_id = model.Id,
                category_name = model.Name
            });

            return result;
        }
    }
}
