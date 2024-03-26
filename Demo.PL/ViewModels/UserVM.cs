using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Demo.PL.ViewModels
{
	public class UserVM
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[ValidateNever]
        public IEnumerable<string> Roles { get; set; }

    }
}
