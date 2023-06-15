

using AutoMapper;
using Azure;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.DataAccess.Abstract;
using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;


namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
      
        private readonly IMapper _mapper;
        private readonly GameSaleDbContext _gameSaleDbContext;
       
        public CategoryService(ICategoryDal categoryDal, IMapper mapper, GameSaleDbContext gameSaleDbContext)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
            _gameSaleDbContext = gameSaleDbContext;
            
            
        }

        public async Task<bool> AddCategory(Category dto)
        {

            try
            {
               CategoryValidator validations = new CategoryValidator();
                var validationResult = validations.Validate(dto);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);

                }

                if (await GetCategoryByName(dto.CategoryName) == null)
                {
                    await _categoryDal.Add(_mapper.Map<Entities.Concrete.Models.Category>(dto));
                   
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
         
        }

       

        public async Task DeleteCategory(int id)
        {
            var removedEntity =  await _categoryDal.GetWithAsNoTracking(id);
            if (removedEntity != null)
            {
               _categoryDal.Delete(removedEntity);
               
            }
            else
                throw new NotImplementedException("hata");
        }

      

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = _mapper.Map <CategoryDto> (await _categoryDal.Get(x => x.CategoryId == id));
            return category;

        }

        public async Task<CategoryDto> GetCategoryByName(string name)
        {
            var category = _mapper.Map<CategoryDto>(await _categoryDal.Get(x => x.CategoryName.Contains(name)));

            return category;
        }

        public async Task<List<CategoryListDto>> GetCategoryList()
        {
            var categoryList = _mapper.Map<List<CategoryListDto>>(await _categoryDal.GetList());
            return categoryList;
        }

        public async Task UpdateCategory(Category model)
        {
            CategoryValidator validations = new CategoryValidator();
            var validationResult = validations.Validate(model);

            if (validationResult.IsValid)
            {
                var updatedEntity = await _categoryDal.GetWithAsNoTracking(model.CategoryId);
                if (updatedEntity != null)
                {
                    await _categoryDal.Update(model);
                    await _gameSaleDbContext.SaveChangesAsync();

                }

            }
            else
                throw new NotImplementedException();

        }

     
    }

      
    }

