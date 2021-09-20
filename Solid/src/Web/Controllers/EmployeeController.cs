using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models.Employee;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository _repository;

        public EmployeeController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IActionResult ListEmployees(ListEmployeeFilter filter)
        {
            var employees = _repository.Employees;

            if (filter.DepartmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == filter.DepartmentId.Value);
            }

            if (filter.EmployedSince.HasValue)
            {
                employees = employees.Where(e => e.EmploymentStartDate >= filter.EmployedSince.Value);
            }

            if (filter.Type.HasValue)
            {
                employees = employees.Where(e => e.Type == filter.Type.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                employees = employees.Where(e => e.Name.Contains(filter.Name));
            }

            var model = new ListEmployeeViewModel();

            model.Employees = employees.Select(i => new ListEmployeeItem
            {
                Name = i.Name,
                Department = i.Department.Name,
                Type = i.Type,
                HireDate = i.EmploymentStartDate
            }).ToArray();

            model.Filter = filter;
            model.Departments = _repository.Departments.ToArray()
                .Select(i => new SelectListItem() {Text = i.Name, Value = i.Id.ToString()});
            model.EmployeeTypes = ((EmployeeType[]) Enum.GetValues(typeof(EmployeeType))).Select(i => new SelectListItem
                {Text = i.ToString("G"), Value = ((int) i).ToString()});

            return View(model);
        }

        [HttpGet]
        public IActionResult CalculateSalary(CalculateSalaryFilter filter)
        {
            var model = new CalculateSalaryViewModel();

            model.Filter = filter;
            model.Departments = _repository.Departments.ToArray()
                .Select(i => new SelectListItem() { Text = i.Name, Value = i.Id.ToString() });
            model.EmployeeItems = _repository.Employees.ToArray()
                .Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            model.Months = Enumerable.Range(1, 12).Select(i => new SelectListItem
                { Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(i), Value = i.ToString() });

            var minYear = _repository.Employees.Min(e => e.EmploymentStartDate);
            model.Years = Enumerable.Range(minYear.Year, DateTime.Now.Year - minYear.Year + 1)
                .Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var periodStart = new DateTime(filter.Year.Value, filter.Month.Value, 1);
            var periodEnd = periodStart.AddMonths(1).AddSeconds(-1);

            var employees = _repository.Employees.Include(e => e.TimeReports).Include(e => e.SalesDeals)
                .Where(e => e.EmploymentStartDate <= periodStart && e.IsActive);

            if (filter.DepartmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == filter.DepartmentId.Value);
            }

            if (filter.EmployeeId.HasValue)
            {
                employees = employees.Where(e => e.Id == filter.EmployeeId.Value);
            }

            var employeeItems = employees.ToArray();

            var employeeModels = new List<CalculateSalaryEmployeeItem>();
            foreach (var employee in employeeItems)
            {
                if (employee.Type == EmployeeType.Manager)
                {
                    var time = employee.TimeReports.Where(t => t.Date >= periodStart && t.Date < periodEnd).ToArray();

                    var monthlySalary = employee.ManagerMonthlySalary.Value;
                    
                    var vacationDays = time.Where(t => t.Type == TimeReportType.Vacation);
                    var managerSalary = monthlySalary 
                                    + vacationDays.Count() * (monthlySalary / 20) * 1.2M 
                                    - vacationDays.Count() * (monthlySalary / 20);

                    var sickDays = time.Where(t => t.Type == TimeReportType.SickDay);
                    managerSalary = managerSalary
                                        + sickDays.Count() * (monthlySalary / 20) * 0.8M
                                        - sickDays.Count() * (monthlySalary / 20);

                    employeeModels.Add(new CalculateSalaryEmployeeItem {Name = employee.Name, Department = employee.Department.Name, Type = employee.Type, Salary = managerSalary});
                }
                else if (employee.Type == EmployeeType.Standard)
                {
                    var time = employee.TimeReports.Where(t => t.Date >= periodStart && t.Date < periodEnd).ToArray();

                    var monthlySalary = employee.StandardMonthlySalary.Value;

                    var vacationDays = time.Where(t => t.Type == TimeReportType.Vacation);
                    var standardSalary = monthlySalary
                                        + vacationDays.Count() * (monthlySalary / 20) * 1.2M
                                        - vacationDays.Count() * (monthlySalary / 20);

                    var sickDays = time.Where(t => t.Type == TimeReportType.SickDay);
                    standardSalary = standardSalary
                                    + sickDays.Count() * (monthlySalary / 20) * 0.8M
                                    - sickDays.Count() * (monthlySalary / 20);

                    employeeModels.Add(new CalculateSalaryEmployeeItem { Name = employee.Name, Department = employee.Department.Name, Type = employee.Type, Salary = standardSalary });
                }
                else if (employee.Type == EmployeeType.Sales)
                {
                    var time = employee.TimeReports.Where(t => t.Date >= periodStart && t.Date < periodEnd).ToArray();

                    var monthlyBaseSalary = employee.SalesBaseSalary.Value;

                    var vacationDays = time.Where(t => t.Type == TimeReportType.Vacation);
                    var baseSalary = monthlyBaseSalary
                                         + vacationDays.Count() * (monthlyBaseSalary / 20) * 1.2M
                                         - vacationDays.Count() * (monthlyBaseSalary / 20);

                    var sickDays = time.Where(t => t.Type == TimeReportType.SickDay);
                    baseSalary = baseSalary
                                     + sickDays.Count() * (monthlyBaseSalary / 20) * 0.8M
                                     - sickDays.Count() * (monthlyBaseSalary / 20);

                    var deals = employee.SalesDeals.Where(d => d.AgreementDate >= periodStart && d.AgreementDate < periodEnd).ToArray();

                    var totalDealsPrice = deals.Sum(d => d.Price);

                    var provisionSalary = (decimal)employee.SalesProvision.Value / 100 * totalDealsPrice;

                    employeeModels.Add(new CalculateSalaryEmployeeItem { Name = employee.Name, Department = employee.Department.Name, Type = employee.Type, Salary = baseSalary + provisionSalary });
                }
                else if (employee.Type == EmployeeType.Hourly)
                {
                    var time = employee.TimeReports.Where(t => t.Date >= periodStart && t.Date < periodEnd).ToArray();

                    var hourlySalary = employee.HourlySalary.Value;

                    var workedHours = time.Where(t => t.Type == TimeReportType.WorkingHours);

                    var monthlySalary = hourlySalary * workedHours.Sum(h => h.WorkingHours.Value);

                    employeeModels.Add(new CalculateSalaryEmployeeItem { Name = employee.Name, Department = employee.Department.Name, Type = employee.Type, Salary = monthlySalary });
                }
                else if (employee.Type == EmployeeType.Intern)
                {
                    employeeModels.Add(new CalculateSalaryEmployeeItem { Name = employee.Name, Department = employee.Department.Name, Type = employee.Type, Salary = 0 });
                }
            }
            
            model.Employees = employeeModels;

            return View(model);
        }
    }
}
