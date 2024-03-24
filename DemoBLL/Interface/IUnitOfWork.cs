using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IDepartmentReprository Departments { get; }
        public IEmployeeReprository Employees { get; }
        public Task<int> CompleteAsync();
    }
}
