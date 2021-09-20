using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storage.Entities
{
    public interface IBreakAwayContext
    {
        DbSet<Employee> Employees { get; }
        DbSet<Department> Departments { get; }
    }

    public class BreakAwayContext : DbContext, IBreakAwayContext
    {
        public BreakAwayContext(DbContextOptions<BreakAwayContext> options) : base(options)
        {
            Employees = Set<Employee>();
            Departments = Set<Department>();
        }
        
        public DbSet<Employee> Employees { get; }
        public DbSet<Department> Departments { get; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapEmployee(modelBuilder.Entity<Employee>());
            MapDepartment(modelBuilder.Entity<Department>());
            MapDeal(modelBuilder.Entity<Deal>());
            MapTimeReport(modelBuilder.Entity<TimeReport>());
        }

        private void MapEmployee(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employees");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Name).HasColumnName("Name");

            entity.Property(e => e.DepartmentId).HasColumnName("FKDepartmentID");

            entity.Property(e => e.EmploymentStartDate).HasColumnName("EmploymentStartDate");

            entity.Property(e => e.IsActive).HasColumnName("IsActive");

            entity.Property(e => e.Type).HasColumnName("FKTypeID");

            entity.Property(e => e.ManagerMonthlySalary).HasColumnName("ManagerMonthlySalary");

            entity.Property(e => e.StandardMonthlySalary).HasColumnName("StandardMonthlySalary");

            entity.Property(e => e.SalesBaseSalary).HasColumnName("SalesBaseSalary");

            entity.Property(e => e.SalesProvision).HasColumnName("SalesProvision");

            entity.Property(e => e.HourlySalary).HasColumnName("HourlySalary");

            entity.HasOne(e => e.Department).WithMany().HasForeignKey(e => e.DepartmentId);

            entity.HasMany(e => e.TimeReports).WithOne().HasForeignKey(e => e.EmployeeId);

            entity.HasMany(e => e.SalesDeals).WithOne().HasForeignKey(e => e.SalesEmployeeId);
        }

        private void MapDepartment(EntityTypeBuilder<Department> entity)
        {
            entity.ToTable("Departments");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Name).HasColumnName("Name");
        }

        private void MapDeal(EntityTypeBuilder<Deal> entity)
        {
            entity.ToTable("Deals");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.SalesEmployeeId).HasColumnName("FKSalesEmployeeID");

            entity.Property(e => e.CustomerName).HasColumnName("CustomerName");

            entity.Property(e => e.AgreementDate).HasColumnName("AgreementDate");

            entity.Property(e => e.Price).HasColumnName("Price");
        }

        private void MapTimeReport(EntityTypeBuilder<TimeReport> entity)
        {
            entity.ToTable("TimeReports");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Date).HasColumnName("Date");

            entity.Property(e => e.EmployeeId).HasColumnName("FKEmployeeID");

            entity.Property(e => e.Type).HasColumnName("FKTypeID");

            entity.Property(e => e.WorkingHours).HasColumnName("HoursWorked");
        }
    }
}