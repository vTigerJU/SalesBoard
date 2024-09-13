using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesBoard.Data;
using System.Runtime.Intrinsics.X86;

namespace SalesBoard.Models
{
    public static class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
           
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            SeedSalesStatistics(context);
            await SeedUsers(userManager, context);
            SeedItems(context);
            EnsureRoles(context);
            await EnsureAdmin(userManager);
            context.SaveChanges();

        }

        private static void SeedSalesStatistics(ApplicationDbContext context) 
        {
            if (!context.SalesStatistics.Any())
            {
                SalesStatistics statistics = new SalesStatistics();
                statistics.SalesMade = 0;
                statistics.Commission = 0;
                statistics.CommissionRate = 0.01;
                context.SalesStatistics.Add(statistics);
                context.SaveChanges();

            }
        }
        private static void SeedItems(ApplicationDbContext context) 
        { 
            var users = context.Users.ToArray();
            if (!users.IsNullOrEmpty() && !context.Item.Any())
            {
                context.Item.AddRange(
                   new Item
                   {
                       Name = "Ball",
                       Description = "Round and bouncy",
                       Price = 10,
                       Quantity = 5,
                       User = users[0]
                   },
                   new Item
                   {
                       Name = "Not Ball",
                       Description = "Square and not bouncy",
                       Price = 100,
                       Quantity = 2,
                       User = users[0]

                   },
                   new Item
                   {
                       Name = "Bike",
                       Description = "Needs minor repairs",
                       Price = 30,
                       Quantity = 1,
                       User = users[1]

                   }
               );
                context.SaveChanges();


            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var userEmail = "user1@gmail.com";
                var userPassword = "Hi123!";
                var user1 = new ApplicationUser
                {
                    Name = "John Doe",
                    NormalizedUserName = userEmail.ToUpper(),
                    UserName = userEmail,
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpper()
                };
                userEmail = "user2@gmail.com";
                var user2 = new ApplicationUser
                {
                    Name = "Smith Smithson",
                    NormalizedUserName = userEmail.ToUpper(),
                    UserName = userEmail,
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpper()
                };

                var result = await userManager.CreateAsync(user1, userPassword);
                if (result.Succeeded) { await userManager.AddToRoleAsync(user1, "User"); }
                result = await userManager.CreateAsync(user2, userPassword);
                if (result.Succeeded) { await userManager.AddToRoleAsync(user2, "User"); }
                context.SaveChanges();

              
            }

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
                context.SaveChanges();

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
