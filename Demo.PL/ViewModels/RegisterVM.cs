using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterVM
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is Required") , EmailAddress(ErrorMessage = "InValid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage = "Confirmation is Required")]
		[DataType(DataType.Password)]
		[Compare("Password" , ErrorMessage ="Confirm Password Doesnt Match Password")]
		public string ConfirmPassword { get; set; }
        public bool Agree { get; set; }
    }
}
