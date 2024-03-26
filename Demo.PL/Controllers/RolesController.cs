using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class RolesController : Controller
	{ 
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<IdentityRole> roleManager , IMapper mapper)
		{
            _roleManager = roleManager;
            _mapper = mapper;
        }

		public async Task<IActionResult> Index(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
                //AppUser => RoleVM
                //Select Operator {Transformation Operator}
                var roles = await _roleManager.Roles.Select(r => new RoleVM
				{
					Id = r.Id,
					Name = r.Name,

				}).ToListAsync();
				return View(roles);
			}

			var role = await _roleManager.FindByNameAsync(name);
			if (role == null)
				return View(Enumerable.Empty<RoleVM>());
			var mappedUser = new RoleVM()
			{
                Id = role.Id,
                Name = role.Name,

            };
			return View(new List<RoleVM> { mappedUser });
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Create(RoleVM model)
        {
			if (ModelState.IsValid)
			{
				var mapRole = _mapper.Map<IdentityRole>(model);
				var result = await _roleManager.CreateAsync(mapRole);

				if (result.Succeeded)
					return RedirectToAction(nameof(Index));

				foreach (var role in result.Errors)
					ModelState.AddModelError("", role.Description);
			}
            return View(model);
        }

        public async Task<IActionResult> Details (string id , string ViewName = "Details")
		{
			if (string.IsNullOrWhiteSpace(id)) return BadRequest();

			var role =await _roleManager.FindByIdAsync(id);
			if (role == null) return NotFound();

			var mapRole = _mapper.Map<IdentityRole , RoleVM>(role);
			return View(ViewName , mapRole);
		}

		public async Task<IActionResult> Edit(string id)
		{
			return await Details(id , nameof(Edit));
		}


		[HttpPost]
        public async Task<IActionResult> Edit([FromRoute]string id , RoleVM model)
        {
			if(id != model.Id) return BadRequest();
			if (!ModelState.IsValid) return View(model);

			//Add Users to Role

			try
			{
				var role = await _roleManager.FindByIdAsync(id);
				if (role is null) return NotFound();

				role.Name = model.Name;

				await _roleManager.UpdateAsync(role);
				return RedirectToAction(nameof (Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("" , ex.Message);
			}
			return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, nameof(Delete));
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return NotFound();

                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }
    }
}
