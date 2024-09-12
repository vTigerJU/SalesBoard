using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesBoard.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string BuyerId { get; set; }

        [ForeignKey("ApplicationUser")]
        public ApplicationUser Seller { get; set; }
        [DisplayName("Money spent")]
        public double MoneySpent { get; set; } = 0;

    }
}
