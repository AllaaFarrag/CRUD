using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column(TypeName ="Money")]
        public decimal Salary { get; set; }

        public int Age { get; set; }
        public string Address { get; set; }  

        public string Email { get; set; }

        public string Phone { get; set; }
        public bool IsAcive { get; set; }

        public DateTime HireDate { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }

        public string? ImageName { get; set; }
    }
}
