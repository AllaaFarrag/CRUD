using Demo.DAL.Entites;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        [Range(10, 100)]
        public int Code { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee> { };
    }
}
