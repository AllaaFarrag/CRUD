using Demo.BLL.Interface;
using Demo.DAL.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reprository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CompanyDbContext _dbContext;

        public GenericRepository(CompanyDbContext dbContext  )
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
           await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbContext.Employees.Include(e => e.Department).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

    }
}
