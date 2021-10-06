using System.Linq;

namespace Domain
{
    public interface IRepository
    {
        IQueryable<Employee> Employees { get; }
        IQueryable<Department> Departments { get; }
    }
}