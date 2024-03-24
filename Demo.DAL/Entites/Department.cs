using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Code { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee> { };

    }
}
