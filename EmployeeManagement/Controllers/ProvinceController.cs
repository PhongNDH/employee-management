using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;
using EmployeeManagement.Utils.Constant;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly IGenericService<Province> _provinceService;

        public ProvinceController(IGenericService<Province> provinceService)
        {
            _provinceService = provinceService;
        }

        public async Task<IActionResult> Index(int page, int size)
        {
            var provinces = await _provinceService.GetEntityListAsync(page, size);
            return View(provinces);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Province province)
        {
            if (!await _provinceService.CreateEntityAsync(province))
            {
                // ViewBag.EthnicGroups = await _ethnicGroupService.GetEntityListAsync();
                // ViewBag.Occupations = await _occupationService.GetEntityListAsync();
                // ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                // ViewBag.Districts = await _districtService.GetEntityListAsync();
                // ViewBag.Communes = await _communeService.GetEntityListAsync();
                return View(province);
            }

            TempData["success"] = "Province created successfully";
            return RedirectToAction("Index", "Province", new { page = 1, size = Constant.SizeOfProvincePage });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var province = await _provinceService.GetEntityByIdAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Province province)
        {
            if (!await _provinceService.UpdateEntityAsync(province))
            {
                return View(province);
            }

            TempData["success"] = "Province updated successfully";
            return RedirectToAction("Index", "Province", new { page = 1, size = Constant.SizeOfProvincePage });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var province = await _provinceService.GetEntityByIdAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var province = await _provinceService.GetEntityByIdAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            await _provinceService.DeleteEntityAsync(province);

            TempData["success"] = "Province deleted successfully";
            return RedirectToAction("Index", "Province", new { page = 1, size = Constant.SizeOfProvincePage });
        }
    }
}