using System.Net.Http.Headers;

namespace SalesBoard.Models
{
    public interface ICartService
    {
        void AddItem(Item item, int quantity);
        void RemoveItem(int itemId);
        void ClearCart();
        void CheckOut();
        void BuyItem(CartItem cartItem);
        Cart GetCart();
    }
}
