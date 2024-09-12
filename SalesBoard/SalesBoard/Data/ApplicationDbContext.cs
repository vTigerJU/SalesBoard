using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesBoard.Models;

namespace SalesBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SalesBoard.Models.Item> Item { get; set; } = default!;
        public DbSet<SalesBoard.Models.Customer> Customer { get; set; }
        public DbSet<SalesBoard.Models.Cart> Cart { get; set; }
        public DbSet<SalesBoard.Models.CartItem> CartItem { get; set; }
        public DbSet<SalesBoard.Models.SalesStatistics> SalesStatistics { get; set; }
    }
}
