using ClosedXML.Excel;
using EmployeeManagement.DataAccess.SeedData;
using EmployeeManagement.DataAccess.Specification;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;
using EmployeeManagement.Utils;
using EmployeeManagement.Utils.Constant;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericService<Employee> _employeeService;
        private readonly IGenericService<Occupation> _occupationService;
        private readonly IGenericService<EthnicGroup> _ethnicGroupService;
        private readonly IGenericService<Province> _provinceService;
        private readonly IGenericService<District> _districtService;
        private readonly IGenericService<Commune> _communeService;

        public EmployeeController(IGenericService<Employee> employeeService,
            IGenericService<Occupation> occupationService, IGenericService<EthnicGroup> ethnicGroupService,
            IGenericService<Province> provinceService, IGenericService<District> districtService,
            IGenericService<Commune> communeService)
        {
            _employeeService = employeeService;
            _occupationService = occupationService;
            _ethnicGroupService = ethnicGroupService;
            _provinceService = provinceService;
            _districtService = districtService;
            _communeService = communeService;
        }

        public async Task<IActionResult> Index(int page, int size)
        {
            var spec = new EmployeeDetailSpecification();
            var employees = await _employeeService.GetEntityListWithSpecification(spec,page, size);
            ViewBag.CurrentPage = page;
            ViewBag.NumberOfPage = (int)Math.Ceiling((float)_employeeService.GetEntityListAsync().Result.Count / size);
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.EthnicGroups = await _ethnicGroupService.GetEntityListAsync();
            ViewBag.Occupations = await _occupationService.GetEntityListAsync();
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Districts = await _districtService.GetEntityListAsync();
            ViewBag.Communes = await _communeService.GetEntityListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!await _employeeService.CreateEntityAsync(employee))
            {
                ViewBag.EthnicGroups = await _ethnicGroupService.GetEntityListAsync();
                ViewBag.Occupations = await _occupationService.GetEntityListAsync();
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                ViewBag.Districts = await _districtService.GetEntityListAsync();
                ViewBag.Communes = await _communeService.GetEntityListAsync();
                return View(employee);
            }

            TempData["success"] = "Employee created successfully";
            return RedirectToAction("Index", "Employee", new { page = 1, size = Constant.SizeOfEmployeePage });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            ViewBag.EthnicGroups = await _ethnicGroupService.GetEntityListAsync();
            ViewBag.Occupations = await _occupationService.GetEntityListAsync();
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Districts = await _districtService.GetEntityListAsync();
            ViewBag.Communes = await _communeService.GetEntityListAsync();

            var spec = new EmployeeDetailSpecification(id);
            var employee = await _employeeService.GetEntityByIdWithSpecification(spec);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!await _employeeService.UpdateEntityAsync(employee))
            {
                ViewBag.EthnicGroups = await _ethnicGroupService.GetEntityListAsync();
                ViewBag.Occupations = await _occupationService.GetEntityListAsync();
                ViewBag.Provinces = await _provinceService.GetEntityListAsync();
                ViewBag.Districts = await _districtService.GetEntityListAsync();
                ViewBag.Communes = await _communeService.GetEntityListAsync();

                var spec = new EmployeeDetailSpecification(employee.Id);
                var currentEmployee = await _employeeService.GetEntityByIdWithSpecification(spec);
                return View(currentEmployee);
            }

            TempData["success"] = "Employee updated successfully";
            return RedirectToAction("Index", "Employee", new { page = 1, size = Constant.SizeOfEmployeePage });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            ViewBag.EthnicGroups = await _ethnicGroupService.GetEntityListAsync();
            ViewBag.Occupations = await _occupationService.GetEntityListAsync();
            ViewBag.Provinces = await _provinceService.GetEntityListAsync();
            ViewBag.Districts = await _districtService.GetEntityListAsync();
            ViewBag.Communes = await _communeService.GetEntityListAsync();
            var employee = await _employeeService.GetEntityByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var employee = await _employeeService.GetEntityByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEntityAsync(employee);

            TempData["success"] = "Employee deleted successfully";
            return RedirectToAction("Index", "Employee", new { page = 1, size = Constant.SizeOfEmployeePage });
        }

        public async Task ExportEmployeeToExcel()
        {
            var spec = new EmployeeDetailSpecification();
            var employeeList = await _employeeService.GetEntityListWithSpecification(spec);
            ImportExportExcel.ExportEmployee(employeeList);
            TempData["success"] = "Employee exported successfully to " + Constant.SaveFileName;
        }
        
        //[HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile? file)
        {
            if (file is { Length: > 0 })
            {
                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                try
                {
                    var workbook = new XLWorkbook(memoryStream);
                    var ethnicGroups = await _ethnicGroupService.GetEntityListAsync();
                    var occupations = await _occupationService.GetEntityListAsync();
                    var communes = await _communeService.GetEntityListAsync();


                    var response =
                        ImportExportExcel.ImportEmployee(workbook, ethnicGroups, occupations, communes);
                    if (response.ErrorMessage != null || response.Employee.Count < 1)
                    {
                        TempData["error"] =$"Employee imported fail from {file.FileName}:  \n" + response.ErrorMessage?.Content;
                        return RedirectToAction("Index", "Employee",new { page = 1, size = Constant.SizeOfEmployeePage} );
                    }
                    await _employeeService.DeleteAllEntityAsync();
                    await _employeeService.InitialiseEntityAsync(response.Employee);
                    TempData["success"] = "Employee imported successfully from " + file.FileName;
                    return RedirectToAction("Index", "Employee",new { page = 1, size = Constant.SizeOfEmployeePage} );
                }
                catch (Exception)
                {
                    TempData["error"] = "Please choose other type of file";
                    return RedirectToAction("Index", "Employee",new { page = 1, size = Constant.SizeOfEmployeePage} );
                }
            }

            TempData["error"] = "Please choose other type of file";
            return NotFound();
        }
    }
}