using Business.ValidationRules.FluentValidation;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(Category dto);
        Task UpdateCategory(Category model);
        Task DeleteCategory(int id);
        Task<CategoryDto> GetCategoryById(int id);
        Task<CategoryDto> GetCategoryByName(string name);
        Task<List<CategoryListDto>> GetCategoryList();
    }
}
