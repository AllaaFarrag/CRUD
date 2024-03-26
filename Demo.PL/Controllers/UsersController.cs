using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager , IMapper mapper)
		{
			_userManager = userManager;
            _mapper = mapper;
        }

		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				//AppUser => UserVM
				//Select Operator {Transformation Operator}
				var users = await _userManager.Users.Select(u => new UserVM
				{
					Id = u.Id,
					Email = u.Email,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Roles = _userManager.GetRolesAsync(u).Result
				}).ToListAsync();
				return View(users);
			}

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return View(Enumerable.Empty<UserVM>());
			var mappedUser = new UserVM()
			{
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Id = user.Id,
				Roles = _userManager.GetRolesAsync(user).Result

			};
			return View(new List<UserVM> { mappedUser });
		}

		public async Task<IActionResult> Details ([FromRoute]string id , string ViewName = "Details")
		{
			if (string.IsNullOrWhiteSpace(id)) return BadRequest();

			var user =await _userManager.FindByIdAsync(id);
			if (user == null) return NotFound();

			var mapUser = _mapper.Map<AppUser , UserVM>(user);
			mapUser.Roles = await _userManager.GetRolesAsync(user);
			return View(ViewName , mapUser);
		}

		public async Task<IActionResult> Edit( string id)
		{
			return await Details(id , nameof(Edit));
		}


		[HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id , UserVM model)
        {
			if(id != model.Id) return BadRequest();
			if (!ModelState.IsValid) return View(model);

			try
			{
				var user = await _userManager.FindByIdAsync(id);
				if (user is null) return NotFound();
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;

				await _userManager.UpdateAsync(user);
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
        public async Task<IActionResult> ConfirmDelete([FromRoute] string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return NotFound();

                await _userManager.DeleteAsync(user);
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
