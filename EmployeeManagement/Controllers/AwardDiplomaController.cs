﻿using EmployeeManagement.DataAccess.Specification;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;
using EmployeeManagement.Utils.Constant;
using Microsoft.AspNetCore.Mvc;



namespace EmployeeManagement.Controllers
{
    public class AwardDiplomaController : Controller
    {
        private readonly IGenericService<Employee> _employeeService;

        private readonly IGenericService<Province> _provinceService;
        private readonly IGenericService<Diploma> _diplomaService;
        private readonly IGenericService<AwardDiploma> _awardDiplomaService;

        public AwardDiplomaController(IGenericService<Employee> employeeService,  IGenericService<Province> provinceService, IGenericService<Diploma> diplomaService, IGenericService<AwardDiploma> awardDiplomaService)
        {
            _employeeService = employeeService;
            _provinceService = provinceService;
            _diplomaService = diplomaService;
            _awardDiplomaService = awardDiplomaService;
        }

        public async Task<IActionResult> Index(int page, int size)
        {
            var spec = new AwardDiplomaDetailSpecification();
            var awardDiplomas = await _awardDiplomaService.GetEntityListWithSpecification(spec, page, size);
            ViewBag.NumberOfPage = (int)Math.Ceiling((float)_awardDiplomaService.GetEntityListAsync().Result.Count / size);
            ViewBag.CurrentPage = page;
            return View(awardDiplomas);
        }

        public async Task<IActionResult> IndexByEmployeeId(int id)
        {
            var spec = new AwardDiplomaDetailSpecification(id.ToString());
            var awardDiplomas = await _awardDiplomaService.GetEntityListWithSpecification(spec);                                      
            ViewBag.CurrentId = id;
            return View(awardDiplomas);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Diplomas = await _diplomaService.GetEntityListAsync();
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Employees = await _employeeService.GetEntityListAsync();
            //ViewBag.EmployeeId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AwardDiploma award)
        {
            if (!await _awardDiplomaService.CreateEntityAsync(award))
            {
                ViewBag.Diplomas = await _diplomaService.GetEntityListAsync();
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                ViewBag.Employees = await _employeeService.GetEntityListAsync();
                return View(award);
            }
            
            TempData["success"] = "Diploma Awarding created successfully";
            return RedirectToAction("Index", "AwardDiploma",new { page = 1, size = Constant.SizeOfAwardDiplomaPage });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            ViewBag.Diplomas = await _diplomaService.GetEntityListAsync();
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Employees = await _employeeService.GetEntityListAsync();
            var award = await _awardDiplomaService.GetEntityByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AwardDiploma award)
        {
            if (!await _awardDiplomaService.UpdateEntityAsync(award))
            {
                ViewBag.Diplomas = await _diplomaService.GetEntityListAsync();
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                ViewBag.Employees = await _employeeService.GetEntityListAsync();
                return View(award);
            }
            TempData["success"] = "Award Diploma updated successfully";

            return RedirectToAction("Index", "AwardDiploma",new { page = 1, size = Constant.SizeOfAwardDiplomaPage });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            ViewBag.Diplomas = await _diplomaService.GetEntityListAsync();
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Employees = await _employeeService.GetEntityListAsync();
            var award = await _awardDiplomaService.GetEntityByIdAsync(id);            
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var award = await _awardDiplomaService.GetEntityByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            
            await _awardDiplomaService.DeleteEntityAsync(award);
            
            TempData["success"] = "Award Diploma deleted successfully";

            return RedirectToAction("Index", "AwardDiploma",new { page = 1, size = Constant.SizeOfAwardDiplomaPage });
        }

        // private async Task<List<T>> GetEntities<T>() where T : class
        // {
        //     return await _dbContext.Set<T>().ToListAsync();
        // }
        //
        // private async Task Validate(AwardDiploma award, bool isUpdateMode)
        // {
        //     List<AwardDiploma> awardingList = await GetEntities<AwardDiploma>();
        //     CheckDuration(award.Duration);
        //     CheckNumberOfDiplomaOfEachEmployee(awardingList, award.EmployeeId);
        //     if(!isUpdateMode)
        //     {
        //         CheckDiplomaIsDuplicated(awardingList, award);
        //     }
        //     
        // }
        //
        // private void CheckDuration(int duration)
        // {
        //     if (duration < 1 || duration > Constant.MaxDuration)
        //     {
        //         ModelState.AddModelError("Duration", "Duration must be from 1 to " + Constant.MaxDuration);
        //     }
        // }
        //
        // private void CheckNumberOfDiplomaOfEachEmployee(List<AwardDiploma> awardingList, int id)
        // {
        //     int numberOfDiplomas = awardingList.Where(a => a.EmployeeId == id && Utils.Validate.GetYear(a.GrantingDate) < a.Duration)
        //                                         .ToList().Count;
        //     if(numberOfDiplomas > Constant.MaxNumberOfDiplomasOfEachEmployee - 1)
        //     {
        //         ModelState.AddModelError("EmployeeId", "Number of diplomas of each employee must not exceed " + Constant.MaxNumberOfDiplomasOfEachEmployee);
        //     }
        // }
        //
        // private void CheckDiplomaIsDuplicated(List<AwardDiploma> awardingList, AwardDiploma award)
        // {
        //     List<AwardDiploma> awardingDiplomas = awardingList.Where(a => a.EmployeeId == award.EmployeeId && Utils.Validate.GetYear(a.GrantingDate) < a.Duration)
        //                                                        .ToList();
        //     if(awardingDiplomas.Count > 0)
        //     {
        //         if(awardingDiplomas.Any(a => a.DiplomaId == award.DiplomaId))
        //         {
        //             ModelState.AddModelError("DiplomaId", "This employee already has been granted this diploma");
        //         }
        //     }
        // }

    }
}
