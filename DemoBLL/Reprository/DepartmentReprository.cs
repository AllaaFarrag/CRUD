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

namespace Demo.BLL.Reprository
{
    public class DepartmentReprository :GenericRepository<Department>, IDepartmentReprository
    {
        public DepartmentReprository(CompanyDbContext context):base(context) 
        {
            
        }

        public async Task<IEnumerable<Department>> GetAllByNameAsync(string name)
        {
            return await _dbContext.Departments.Where(d => d.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetAllByNameAsync(Expression<Func<Department, bool>> expression)
        {
            return await _dbContext.Departments.Where(expression).ToListAsync();
        }

        //private readonly CompanyDbContext DbContext; 

        //public DepartmentReprository(CompanyDbContext dbContext)
        //{
        //    DbContext = dbContext;
        //}
        //public IEnumerable<Department> GetAll()
        //=> DbContext.Departments.ToList();


        //public Department? GetById(int id)
        //{
        //    //var department = DbContext.Departments.FirstOrDefault(x => x.Id == id);
        //    return DbContext.Departments.Find(id);
        //} 
        //public int Add(Department department)
        //{
        //    DbContext.Departments.Add(department);
        //    return DbContext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    DbContext.Departments.Remove(department);
        //    return DbContext.SaveChanges();
        //}

        //public int Update(Department department)
        //{
        //    DbContext.Departments.Update(department);
        //    return DbContext.SaveChanges();
        //}
    }
}
