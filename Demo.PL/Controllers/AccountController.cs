using Demo.DAL.Entites;
using Demo.PL.Utility;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailSettings _mailSettings;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailSettings mailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailSettings = mailSettings;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) //Server Side Validation
                return View(model);

            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Agree = model.Agree,
                UserName = model.FirstName + model.LastName
            };
            //Create User
            var result = await _userManager.CreateAsync(user , model.Password);
            if (result.Succeeded)
                return RedirectToAction(nameof(Login));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description); 

            return View(model);

		}

		public IActionResult Login() => View();

        [HttpPost]
		public async Task<IActionResult> Login(LoginVM model)
		{
            if (!ModelState.IsValid)
			    return View(model);
            
            //Get User

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user is not null)
            {
                //Check user Password
                if (await _userManager.CheckPasswordAsync(user , model.Password ))
                {
                    //Login
                    var result =await _signInManager.PasswordSignInAsync(user,model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
			}
            ModelState.AddModelError("", "InCorrect Email Or Password");
            return View();
		}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

		public IActionResult ForgetPassword() => View();


		[HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if(!ModelState.IsValid) return View(model);

			//get user by email
			var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null)
            {
                //generate password reser link  => /baseUrl/Account/ResetPassword?email=value&token=value
                //generate token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var url = Url.Action("ResetPassword", "Account" , new {email = model.Email , token  } ,Request.Scheme);
                var email = new Email
                {
                    Recipient = model.Email,
                    Subject = "Reset Password",
                    Body = url
                };
                //send email to user
                //MailSetting.SendMail(email);
                _mailSettings.SendMail(email);

                return RedirectToAction(nameof(CheckInbox));

			}
            ModelState.AddModelError("", "Email Doesnt Exist");
			return View();
        }

        public IActionResult CheckInbox() => View();

        public IActionResult ResetPassword(string email, string token) 
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model) 
        {
            if (!ModelState.IsValid) return View();

            var email = TempData["email"] as string;
			var token = TempData["token"]as string;

			var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var result = await _userManager.ResetPasswordAsync(user,token ,model.Password );

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }



	}
}
