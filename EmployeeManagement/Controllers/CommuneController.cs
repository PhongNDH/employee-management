using EmployeeManagement.DataAccess.Specification;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;
using EmployeeManagement.Utils.Constant;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class CommuneController : Controller
    {
        private readonly IGenericService<Province> _provinceService;
        private readonly IGenericService<District> _districtService;
        private readonly IGenericService<Commune> _communeService;

        public CommuneController(
            IGenericService<Province> provinceService, IGenericService<District> districtService,
            IGenericService<Commune> communeService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
            _communeService = communeService;
        }

        public async Task<IActionResult> Index(int page, int size)
        {
            var spec = new CommuneDetailSpecification();
            var communes = await _communeService.GetEntityListWithSpecification(spec, page, size);
            return View(communes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Districts = await _districtService.GetEntityListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Commune commune)
        {
            if (!await _communeService.CreateEntityAsync(commune))
            {
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                ViewBag.Districts = await _districtService.GetEntityListAsync();
                return View(commune);
            }

            TempData["success"] = "Commune created successfully";
            return RedirectToAction("Index", "Commune", new { page = 1, size = Constant.SizeOfCommunePage });
        }

        public async Task<List<District>> UpdateDistrictListAfterProvinceId(int provinceId)
        {
            List<District> districts = await _districtService.GetEntityListAsync();
            return districts.Where(d => d.ProvinceId == provinceId).ToList();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Districts = await _districtService.GetEntityListAsync();
            var spec = new CommuneDetailSpecification(id);
            var commune = await _communeService.GetEntityByIdWithSpecification(spec);
            if (commune == null)
            {
                return NotFound();
            }

            return View(commune);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Commune commune)
        {
            if (!await _communeService.UpdateEntityAsync(commune))
            {
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                ViewBag.Districts = await _districtService.GetEntityListAsync();

                var spec = new CommuneDetailSpecification(commune.Id);
                var currentCommune = await _communeService.GetEntityByIdWithSpecification(spec);

                return View(currentCommune);
            }

            TempData["success"] = "Commune updated successfully";
            return RedirectToAction("Index", "Commune", new { page = 1, size = Constant.SizeOfCommunePage });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Districts = await _districtService.GetEntityListAsync();
            var commune = await _communeService.GetEntityByIdAsync(id);
            if (commune == null)
            {
                return NotFound();
            }

            return View(commune);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var commune = await _communeService.GetEntityByIdAsync(id);
            if (commune == null)
            {
                return NotFound();
            }

            await _communeService.DeleteEntityAsync(commune);

            TempData["success"] = "Commune deleted successfully";

            return RedirectToAction("Index", "Commune", new { page = 1, size = Constant.SizeOfCommunePage });
        }
    }
}