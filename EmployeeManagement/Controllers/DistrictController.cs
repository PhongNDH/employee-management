using EmployeeManagement.DataAccess.Specification;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;
using EmployeeManagement.Utils.Constant;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IGenericService<Province> _provinceService;
        private readonly IGenericService<District> _districtService;

        public DistrictController(IGenericService<Province> provinceService,
            IGenericService<District> districtService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
        }

        public async Task<IActionResult> Index(int page, int size)
        {
            var spec = new DistrictDetailSpecification();
            var districts = await _districtService.GetEntityListWithSpecification(spec, page, size);
            Console.WriteLine(districts.PageTotal);
            return View(districts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(District district)
        {
            if (!await _districtService.CreateEntityAsync(district))
            {
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                return View(district);
            }

            TempData["success"] = "District created successfully";
            return RedirectToAction("Index", "District", new { page = 1, size = Constant.SizeOfDistrictPage });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            var district = await _districtService.GetEntityByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(District district)
        {
            if (!await _districtService.UpdateEntityAsync(district))
            {
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                return View(district);
            }

            TempData["success"] = "District updated successfully";
            return RedirectToAction("Index", "District", new { page = 1, size = Constant.SizeOfDistrictPage });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            var district = await _districtService.GetEntityByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var district = await _districtService.GetEntityByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            await _districtService.DeleteEntityAsync(district);

            TempData["success"] = "District deleted successfully";
            return RedirectToAction("Index", "District", new { page = 1, size = Constant.SizeOfDistrictPage });
        }
    }
}