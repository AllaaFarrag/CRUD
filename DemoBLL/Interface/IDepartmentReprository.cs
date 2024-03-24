using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface IDepartmentReprository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllByNameAsync(string name);
        Task<IEnumerable<Department>> GetAllByNameAsync(Expression<Func<Department,bool>> expression);

        //IEnumerable<Department> GetAll();
        //Department GetById(int id);

        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
    }
}
