using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Employee
{
    public class CalculateSalaryViewModel
    {
        public CalculateSalaryFilter Filter { get; set; }

        public IEnumerable<SelectListItem> EmployeeItems { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> Months { get; set; }
        public IEnumerable<CalculateSalaryEmployeeItem> Employees { get; set; }
    }

    public class CalculateSalaryEmployeeItem
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public EmployeeType Type { get; set; }
        public decimal Salary { get; set; }
    }

    public class CalculateSalaryFilter
    {
        [Required]
        public int? Year { get; set; }
        [Required]
        public int? Month { get; set; }
        public int? EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
    }
}