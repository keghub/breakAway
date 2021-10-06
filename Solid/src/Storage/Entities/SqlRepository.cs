using System;
using System.Linq;
using Domain;

namespace Storage.Entities
{
    public class SqlRepository : IRepository
    {
        private readonly IBreakAwayContext _context;

        public SqlRepository(IBreakAwayContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Employee> Employees => _context.Employees;
        public IQueryable<Department> Departments => _context.Departments;

    }
}
