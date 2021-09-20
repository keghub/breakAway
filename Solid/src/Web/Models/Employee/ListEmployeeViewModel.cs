using System;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Employee
{
    public class ListEmployeeViewModel
    {
        public ListEmployeeFilter Filter { get; set; }
        public IEnumerable<SelectListItem> EmployeeTypes { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }

        public IEnumerable<ListEmployeeItem> Employees { get; set; }
    }

    public class ListEmployeeItem
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public EmployeeType Type { get; set; }
        public DateTime HireDate { get; set; }
    }

    public class ListEmployeeFilter
    {
        public EmployeeType? Type { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? EmployedSince { get; set; }
        public string Name { get; set; }
    }
}