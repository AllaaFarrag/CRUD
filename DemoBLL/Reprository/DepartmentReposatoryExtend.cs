using Demo.BLL.Interface;
using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reprository
{
    public class DepartmentReposatoryExtend : IDepartmentReprository
    {
        public Task AddAsync(Department entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Department entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllByNameAsync(Expression<Func<Department, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
