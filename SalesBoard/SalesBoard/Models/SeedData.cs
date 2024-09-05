using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesBoard.Data;

namespace SalesBoard.Models
{
    public static class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
           
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            EnsureRoles(context);
            await EnsureAdmin(userManager);
            context.SaveChanges();
        }

        private static void EnsureRoles(ApplicationDbContext context)
        {
            // Look for any ´Roles
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                );
            }

        }

        private static async Task EnsureAdmin(UserManager<ApplicationUser> userManager)
        {
            var adminPassword = "Hi123!";
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            Console.WriteLine(adminUser);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Name = "Admin",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    UserName = adminEmail,
                    Email = adminEmail,
                    NormalizedEmail = "ADMIN@GMAIL.COM"
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    
                }
                else
                {
                    throw new Exception("Failed to create admin user");
                }
            }

        }


    }
}
