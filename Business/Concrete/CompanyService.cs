using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.CompanyDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyDal _companyDal;

        private readonly IMapper _mapper;
        private readonly GameSaleDbContext _gameSaleDbContext;
        public CompanyService(ICompanyDal companyDal, IMapper mapper, GameSaleDbContext gameSaleDbContext)
        {
            _companyDal = companyDal;
            _mapper = mapper;
            _gameSaleDbContext = gameSaleDbContext;

        }

        public async Task<bool> AddCompany(Company dto)
        {

            try
            {
                CompanyValidator validations = new CompanyValidator();
                var validationResult = validations.Validate(dto);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);

                }

                if (await GetCompanyByName(dto.CompanyName) == null)
                {
                    await _companyDal.Add(_mapper.Map<Company>(dto));

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public async Task DeleteCompany(int id)
        {
            var removedEntity = await _companyDal.GetWithAsNoTracking(id);
            if (removedEntity != null)
            {
                _companyDal.Delete(removedEntity);

            }
            else
                throw new NotImplementedException("hata");
        }

        public async Task<CompanyDto> GetCompanyById(int id)
        {
            var company = _mapper.Map<CompanyDto>(await _companyDal.Get(x => x.CompanyId == id));
            return company;
        }

        public async Task<CompanyDto> GetCompanyByName(string name)
        {
            var company = _mapper.Map<CompanyDto>(await _companyDal.Get(x => x.CompanyName.Contains(name)));

            return company;
        }

        public async Task<List<CompanyListDto>> GetCompanyList()
        {
            var companylist = _mapper.Map<List<CompanyListDto>>(await _companyDal.GetList());
            return companylist;
        }

        public async Task UpdateCompany(Company model)
        {
            CompanyValidator validations = new CompanyValidator();
            var validationResult = validations.Validate(model);

            if (validationResult.IsValid)
            {
                var updatedEntity = await _companyDal.GetWithAsNoTracking(model.CompanyId);
                if (updatedEntity != null)
                {
                    await _companyDal.Update(model);
                    await _gameSaleDbContext.SaveChangesAsync();

                }

            }
            else
                throw new NotImplementedException();
        }
    }
}