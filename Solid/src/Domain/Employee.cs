using System;
using System.Collections.Generic;

namespace Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public DateTime EmploymentStartDate { get; set; }
        public bool IsActive { get; set; }
        public virtual IEnumerable<TimeReport> TimeReports { get; set; }

        public EmployeeType Type { get; set; }
        
        public decimal? ManagerMonthlySalary { get; set; }

        public decimal? StandardMonthlySalary { get; set; }

        public decimal? SalesBaseSalary { get; set; }
        public decimal? SalesProvision { get; set; }
        public virtual IEnumerable<Deal> SalesDeals { get; set; }

        public decimal? HourlySalary { get; set; }
    }

    public enum EmployeeType
    {
        Manager = 1,
        Standard = 2,
        Sales = 3,
        Hourly = 4,
        Intern = 5
    }
}