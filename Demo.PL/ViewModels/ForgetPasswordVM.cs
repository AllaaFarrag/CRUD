using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ForgetPasswordVM
	{
        [Required(ErrorMessage = "Email is Required"), EmailAddress(ErrorMessage = "InValid Email")]
        public string Email { get; set; }
    }
}
