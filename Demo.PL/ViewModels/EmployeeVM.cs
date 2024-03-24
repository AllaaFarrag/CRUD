using Demo.DAL.Entites;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(30)]
        [MinLength(5)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Range(20, 60)]
        public int Age { get; set; }
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        public bool IsAcive { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageName { get; set; }

        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }


    }
}
