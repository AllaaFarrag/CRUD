using Demo.BLL.Interface;
using Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reprository
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly Lazy<IDepartmentReprository> departments;
        private readonly Lazy<IEmployeeReprository> employees;
        private readonly CompanyDbContext _context;

        //Lazy<IRepository> => initialize repo only when used

        public UnitOfWork(CompanyDbContext context)
        {
            departments = new Lazy<IDepartmentReprository>(new DepartmentReprository(context));
            employees = new Lazy<IEmployeeReprository>(new EmployeeRepository(context));
            _context = context;
        }
        public IDepartmentReprository Departments => departments.Value;

        public IEmployeeReprository Employees => employees.Value;

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();


        //public void Dispose() => _context.Dispose(); // Called by CLR

        public async ValueTask DisposeAsync()
        {
           await _context.DisposeAsync();
        }
    }
}
