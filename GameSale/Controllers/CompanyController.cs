using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.CompanyDtos;
using GameSale.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSale.Controllers
{
    [MyAuthorizationAttribute(MemberType.Admin)]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetCompanyList();
            if (companies == null)
            {
                throw new Exception();
            }
            return View(companies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var company = await _companyService.GetCompanyById(id);
            if (company == null)
            {
                throw new Exception();
            }
            return View(company);
        }

        public  IActionResult Create()
        {
            return View(new CompanyDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company dto)
        {
            if (ModelState.IsValid)
            {
                await _companyService.AddCompany(dto);

                return RedirectToAction("Index");
            }

            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var response = _mapper.Map<Company>(await _companyService.GetCompanyById(id));
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Company response)
        {

            await _companyService.UpdateCompany(response);

            return RedirectToAction("Index");


        }


        public async Task<IActionResult> Delete(int id)
        {
            var response = _mapper.Map<Company>(await _companyService.GetCompanyById(id));
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string ex)
        {

            await _companyService.DeleteCompany(id);

            return RedirectToAction("Index");


        }
    }
}

