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
                PasswordHash = "AQAAAAEAACcQAAAAEBaA5EMstOiPliZR3Whk+8FaW5S25TK7r1dN1fjdCLwLNLfAfBSixQKhDMiYadQeOQ==",
                SecurityStamp = "O4JPUURPA3NEWQ5IR2RPDHEO7WLZYBWV",
                ConcurrencyStamp = "773338ee-7657-421d-b718-e919e7633ede"
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

