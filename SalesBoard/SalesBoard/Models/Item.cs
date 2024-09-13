using System.ComponentModel.DataAnnotations;

namespace SalesBoard.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="Only positive numbers allowed")]
        public int Quantity { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
