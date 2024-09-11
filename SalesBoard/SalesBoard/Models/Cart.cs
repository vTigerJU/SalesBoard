using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesBoard.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public virtual IList<CartItem> CartItems { get; set; } = new List<CartItem>();
        [ForeignKey("ApplicationUser")]
        public ApplicationUser? User { get; set; }

    }
}
