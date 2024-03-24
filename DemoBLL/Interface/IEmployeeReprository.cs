using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface IEmployeeReprository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllByNameAsync(string name);
        Task<IEnumerable<Employee>> GetAllByNameAsync(Expression<Func<Employee,bool>> expression );

        //IEnumerable<Employee> GetAll();
        //Employee GetById(int id);

        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);
    }
}
