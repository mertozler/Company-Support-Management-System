using System;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyPanelUI.Data
{
    public class SeedDataForIdentity
    {
        private ApplicationDbContext _context;

        public SeedDataForIdentity(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void SeedAdminUser()
        {
            var user = new CustomUser
            {
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "admin"))
            {
                await roleStore.CreateAsync(new IdentityRole {Name = "admin", NormalizedName = "ADMIN"});
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<CustomUser>();
                var hashed = password.HashPassword(user, "123Mer.");
                user.PasswordHash = hashed;
                var userStore = new UserStore<CustomUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "admin");
            }

            await _context.SaveChangesAsync();
        }
    }
}
