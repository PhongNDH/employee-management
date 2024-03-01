using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;
using EmployeeManagement.Utils.Constant;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DiplomaController : Controller
    {
        private readonly IGenericService<Diploma> _diplomaService;

        public DiplomaController(IGenericService<Diploma> diplomaService)
        {
            _diplomaService = diplomaService;
        }
        public async Task<IActionResult> Index(int page, int size)
        {
            var diplomas = await _diplomaService.GetEntityListAsync(page, size);
            return View(diplomas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Diploma diploma)
        {
            if (!await _diplomaService.CreateEntityAsync(diploma))
            {
                return View(diploma);
            }
            TempData["success"] = "Diploma created successfully";
            return RedirectToAction("Index", "Diploma", new { page = 1, size = Constant.SizeOfDiplomaPage });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var diploma = await _diplomaService.GetEntityByIdAsync(id);
            if (diploma == null)
            {
                return NotFound();
            }
            return View(diploma);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Diploma diploma)
        {
            if (!await _diplomaService.UpdateEntityAsync(diploma))
            {
                return View(diploma);
            }
            TempData["success"] = "Diploma updated successfully";

            return RedirectToAction("Index", "Diploma", new { page = 1, size = Constant.SizeOfDiplomaPage });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var diploma = await _diplomaService.GetEntityByIdAsync(id);
            if (diploma == null)
            {
                return NotFound();
            }
            return View(diploma);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var diploma = await _diplomaService.GetEntityByIdAsync(id);
            if (diploma == null)
            {
                return NotFound();
            }

            await _diplomaService.DeleteEntityAsync(diploma);
            
            TempData["success"] = "Diploma deleted successfully";

            return RedirectToAction("Index", "Diploma", new { page = 1, size = Constant.SizeOfDiplomaPage });
        }
    }
}
