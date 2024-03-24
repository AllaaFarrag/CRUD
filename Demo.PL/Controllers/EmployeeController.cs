using AutoMapper;
using Demo.BLL.Interface;
using Demo.DAL.Entites;
using Demo.PL.Utility;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeReprository _reprository;
        //private readonly IDepartmentReprository _department;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(/*IEmployeeReprository reprository , IDepartmentReprository department,*/ IMapper mapper
            ,IUnitOfWork unitOfWork)
        {
            //_reprository = reprository;
            //_department = department;
            _mapper = mapper;
            _unitOfWork = unitOfWork; 
        }
        public async Task<IActionResult> Index(string? searchView)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(searchView))
            {
                employees = await _unitOfWork.Employees.GetAllAsync();
                return View(_mapper.Map<IEnumerable<EmployeeVM>>(employees));
            }

            //ViewBag.Department = _department.GetAll();

            employees =await _unitOfWork.Employees.GetAllByNameAsync(e => e.Name.ToLower().Contains(searchView.ToLower()));
            return View(_mapper.Map<IEnumerable<EmployeeVM>>(employees));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Department = await _unitOfWork.Departments.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeeVm)
        {
            if (ModelState.IsValid)
            {
                if (employeeVm.Image is not null)
                {
                    employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");

                }
                var employee = _mapper.Map<EmployeeVM , Employee>(employeeVm);
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.CompleteAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVm);
        }

        public async Task<IActionResult> Details(int? id) =>await ReturnViewWithEmployee(id,nameof(Details));


        public async Task<IActionResult> Edit(int? id) =>await ReturnViewWithEmployee(id,nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeVM employeeVm , [FromRoute] int id)
        {
            if(id !=employeeVm.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVm.Image is not null)
                    {
                        employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");

                    }
                    _unitOfWork.Employees.Update(_mapper.Map<EmployeeVM, Employee>(employeeVm));
                    await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(employeeVm);
        }

        public async Task<IActionResult> Delete(int? id) =>await ReturnViewWithEmployee(id,nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete (EmployeeVM employeeVm , [FromRoute] int id)
        {
            if (id !=employeeVm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Employees.Delete(_mapper.Map<EmployeeVM, Employee>(employeeVm));

                    if (await _unitOfWork.CompleteAsync() > 0 && employeeVm.ImageName is not null)
                        DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("" ,ex.Message);
                }
            }
            return View(employeeVm);
        }

        private async Task<IActionResult> ReturnViewWithEmployee(int? id, string viewName)
        {
            if (!id.HasValue) return BadRequest();

            var employee =await _unitOfWork.Employees.GetByIdAsync(id.Value);

            if (employee == null) return NotFound();

            ViewBag.Department = await _unitOfWork.Departments.GetAllAsync();

            return View(viewName, _mapper.Map<EmployeeVM>(employee));
        }
    }
}
