using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ResetPasswordVM
	{
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirmation is Required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Confirm Password Doesnt Match Password")]
		public string ConfirmPassword { get; set; }
	}
}
