using AutoMapper;
using Demo.BLL.Interface;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentReprository _reprository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(/*IDepartmentReprository reprository */ IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_reprository = reprository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? searchView) //All Dept
        {
            //ViewData["Message"] = new Employee() { Name = "Blah blah"};  //Dictionary based on object
            //ViewBag.Messaage = "Hello From ViewBag";   //Dynamic object can store any data type

            IEnumerable<Department> departments;
            if (string.IsNullOrWhiteSpace(searchView))
            {
                departments = await _unitOfWork.Departments.GetAllAsync();
                var deptMap1 = _mapper.Map<IEnumerable<DepartmentVM>>(departments);
                return View(deptMap1);
            }
            departments =  await _unitOfWork.Departments.GetAllByNameAsync(d => d.Name.ToLower().Contains(searchView.ToLower()));
            var deptMap = _mapper.Map<IEnumerable<DepartmentVM>>(departments);
            return View(deptMap);
        }
        [Authorize]
		public IActionResult Create()
        {
            return View();
        }

		[Authorize]
		[HttpPost]
        public async Task<IActionResult> Create(DepartmentVM departmentVm)
        {
            if(ModelState.IsValid)
            {
                var DeptMap = _mapper.Map<DepartmentVM, Department>(departmentVm);
               /* var result =*/await _unitOfWork.Departments.AddAsync(DeptMap);
                await _unitOfWork.CompleteAsync();
                //if (result > 0)
                //{
                //    TempData["Message"] = "Department Added Successfully";   //From Action to Action
                //}
                return Redirect(nameof(Index));
            }

            return View(departmentVm);
        }

        public async Task<IActionResult> Details(int? id) => await ReturnViewWithDepartment(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id) => await ReturnViewWithDepartment(id, nameof(Edit));

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentVM departmentVm, [FromRoute] int id)
        {
            if(id != departmentVm.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var DeptMap = _mapper.Map<DepartmentVM, Department>(departmentVm);
                    _unitOfWork.Departments.Update(DeptMap);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("" ,ex.Message);
                }
                
            }
            return View(departmentVm);
        }

        public async Task<IActionResult> Delete(int? id) => await ReturnViewWithDepartment(id, nameof(Delete));

        [HttpPost]
        public IActionResult Delete(DepartmentVM departmentVm, [FromRoute] int id)
        {
            try
            {
                var DeptMap = _mapper.Map<DepartmentVM, Department>(departmentVm);
                /*int result = */_unitOfWork.Departments.Delete(DeptMap);
                _unitOfWork.CompleteAsync();

                //if (result > 0)
                //    TempData["Del"] = "Department Deleted Successfully";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(departmentVm);
        }

        private async Task<IActionResult> ReturnViewWithDepartment(int? id, string viewName)
        {
            if (!id.HasValue) return BadRequest();

            var department = await _unitOfWork.Departments.GetByIdAsync(id.Value);

            if (department == null) return NotFound();

            var DeptMap = _mapper.Map<DepartmentVM>(department);
            return View(viewName, DeptMap);
        }
    }
}
