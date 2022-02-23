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
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=CompanyPanel_DB; integrated security=true;");
        }

        public DbSet<Demand> Demands {get; set;}
        public DbSet<Firm> Firms {get; set;}
        public DbSet<Service> Services {get; set;}
        public DbSet<FirmService> FirmServices {get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<DemandAnswer> DemandAnswers { get; set;}
        public DbSet<DemandFile> DemandFiles { get; set;}

    }
}
