using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer.Concrete
{
    public class Context:DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=CompanyPanelNew_DB; integrated security=true;");
           
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);          


            builder.Entity<CustomUser>().ToTable("CustomUser", build => build.ExcludeFromMigrations());
            builder.Entity<Setting>().HasData(
                new Setting
                {
                    Id = 1,
                    SettingName = "DeleteLogFilterByDay",
                    SettingValue = "2"
                }
            );
           
        }
        

        public DbSet<Demand> Demands {get; set;}
        public DbSet<Firm> Firms {get; set;}
        public DbSet<Service> Services {get; set;}
        public DbSet<FirmService> FirmServices {get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<DemandAnswer> DemandAnswers { get; set;}
        public DbSet<DemandFile> DemandFiles { get; set;}
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentEmployee> DepartmentEmployees { get; set; }
        public DbSet<EmployeeService> EmployeeServices { get; set; }
        public DbSet<EmployeeDemand> EmployeeDemands { get; set; }
        public DbSet<ServiceDepartment> ServiceDepartments { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Logs> Logs { get; set; }
        
        

    }
}
