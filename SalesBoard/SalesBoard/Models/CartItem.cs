namespace SalesBoard.Models
{
    public class CartItem
    {
        public int Id   { get; set; }
        public virtual Item Item { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
