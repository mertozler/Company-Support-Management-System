using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace CompanyPanelUI.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);          


            builder.Entity<CustomUser>().ToTable("CustomUser"); // this will configure the table name of the Identity users table. If you wish you can rename the other tables too.
           
            const string ADMIN_USER_ID = "22e40406-8a9d-2d82-912c-5d6a640ee696";
            // Add an admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "1",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "2",
                Name = "musteri",
                NormalizedName = "MUSTERI"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "3",
                Name = "personel",
                NormalizedName = "PERSONEL"
            });
            // Add a user to be added to the admin role
            builder.Entity<CustomUser>().HasData(new CustomUser
            {
                Id = ADMIN_USER_ID,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                NameSurname = "Sistem Yöneticisi",
                PasswordHash = "AQAAAAEAACcQAAAAEBnB8oXphFdmCsywKjHsM1T0Rqoy+MUE/X6BTKXc92U7kCDqn3k8JwfkAyO3GjGfuA==",
                SecurityStamp = "G4UWDNIBHRMGKMISDT73JLS7P3EBZMRV",
                ConcurrencyStamp = "15142b86-2dd6-4e0a-8731-0af709f5c26b"
            });
            // Add the user to the admin role
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = ADMIN_USER_ID
            });
            
        }
        
       
    }  
}

