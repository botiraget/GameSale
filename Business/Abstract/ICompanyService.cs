using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.CompanyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        Task<bool> AddCompany(Company dto);
        Task UpdateCompany(Company model);
        Task DeleteCompany(int id);
        Task<CompanyDto> GetCompanyById(int id);
        Task<CompanyDto> GetCompanyByName(string name);
        Task<List<CompanyListDto>> GetCompanyList();
    }
}
