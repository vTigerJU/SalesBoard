using System.Net.Http.Headers;

namespace SalesBoard.Models
{
    public interface ICartService
    {
        void AddItem(Item item, int quantity);
        bool RemoveItem(int itemId, int quantity);
        void ClearCart();
        void CheckOut();
        void BuyItem(CartItem cartItem);
        Cart GetCart();
    }
}
