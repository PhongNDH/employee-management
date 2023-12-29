using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.DataAccess.Service;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class UiController : Controller
    {
        private readonly IGenericService<District> _districtService;
        private readonly IGenericService<Commune> _communeService;

        public UiController(DatabaseContext dbContext, IGenericService<District> districtService,
            IGenericService<Commune> communeService)
        {
            _districtService = districtService;
            _communeService = communeService;
        }

        public async Task<List<District>> UpdateDistrictListByProvinceId(int provinceId)
        {
            var districts = await _districtService.GetEntityListAsync();
            return districts.Where(d => d.ProvinceId.Equals(provinceId)).ToList();
        }

        public async Task<List<Commune>> UpdateCommuneListByDistrictId(int districtId)
        {
            var communes = await _communeService.GetEntityListAsync();
            return communes.Where(d => d.DistrictId.Equals(districtId)).ToList();
        }

        public async Task<List<Commune>> UpdateCommuneListByProvinceId(int provinceId)
        {
            var communes = await _communeService.GetEntityListAsync();
            var districts = await _districtService.GetEntityListAsync();
            districts = districts.Where(d => d.ProvinceId.Equals(provinceId)).ToList();
            var firstDistrict = districts.FirstOrDefault();
            return communes.Where(c => c.DistrictId.Equals(firstDistrict?.Id)).ToList();
        }
    }
}