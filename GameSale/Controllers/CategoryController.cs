using AutoMapper;
using Business.Abstract;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using GameSale.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.Metrics;

namespace GameSale.Controllers
{
    [MyAuthorizationAttribute(MemberType.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper
            )
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoryList();
            if (categories == null)
            {
                throw new Exception();
            }
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                throw new Exception();
            }
            return View(category);
        }

        public  IActionResult Create()
        {
            return View(new CategoryDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create(Category dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(dto);

                return RedirectToAction("Index");
            }

            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var response =  _mapper.Map<Category>(await _categoryService.GetCategoryById(id));
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category response)
        {

           await _categoryService.UpdateCategory(response);
           
            return RedirectToAction("Index");


        }

       
        public async Task <IActionResult> Delete(int id)
        {
            var response = _mapper.Map<Category>(await _categoryService.GetCategoryById(id));
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string ex)
        {
            
            await _categoryService.DeleteCategory(id);

            return RedirectToAction("Index");


        }
    }

}
