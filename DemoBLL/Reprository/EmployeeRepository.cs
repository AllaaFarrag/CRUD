using Demo.BLL.Interface;
using Demo.DAL.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.BLL.Reprository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeReprository
    {
        public EmployeeRepository(CompanyDbContext context) :base(context)
        {
            
        }

        public async Task<IEnumerable<Employee>> GetAllByNameAsync(string name)
        {
            return await _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllByNameAsync(Expression<Func<Employee, bool>> expression)
        {
            return await _dbContext.Employees.Include(e => e.Department).Where(expression).ToListAsync();
        }


        //private readonly CompanyDbContext DbContext;

        //public EmployeeRepository(CompanyDbContext dbContext)
        //{
        //    DbContext = dbContext;
        //}

        //public IEnumerable<Employee> GetAll() => DbContext.Employees;

        //public Employee GetById(int id) => DbContext.Employees.Find(id);

        //public int Add(Employee employee)
        //{
        //    DbContext.Employees.Add(employee);
        //    return DbContext.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    DbContext.Employees.Update(employee);
        //    return DbContext.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    DbContext.Employees.Remove(employee);
        //    return DbContext.SaveChanges();
        //}
    }
}
