using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesBoard.Data;

namespace SalesBoard.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any ´Roles
                if (context.Roles.Any())
                {
                    return;   // DB has been seeded
                }
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
    }
}
