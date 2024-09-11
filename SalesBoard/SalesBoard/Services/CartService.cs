using SalesBoard.Data;
using SalesBoard.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace SalesBoard.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public Cart GetCart()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) { return null; }
            var cart = _context.Cart.FirstOrDefault(c => c.User == user);
            if (cart == null)
            {
                cart = new Cart();
                cart.User = user;
                _context.Cart.Add(cart);
                _context.SaveChanges();
            }
            cart.CartItems = _context.CartItem.Where(c => c.Cart == cart ).Include("Item").Include(ci => ci.Item.User).ToList();
            return cart;
        }
        public void AddItem(Item product, int quantity)
        {
            Cart cart = GetCart();
            var item = cart.CartItems.FirstOrDefault(i => i.Item.Id == product.Id);

            if (item == null)
            {
                item = new CartItem { Item = product, Cart = cart ,Quantity = quantity };
                cart.CartItems.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            _context.SaveChanges();
        }
        public void UpdateCart(Cart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Session.SetString("Cart", cartJson);
        }

        public int CartCount()
        {
            var cart = GetCart();
            if (cart == null) return 0;
            return cart.CartItems.Count();
        }

        public void RemoveItem(int itemId)
        {
            var cart = GetCart();
            var itemToRemove = cart.CartItems.FirstOrDefault(ci => ci.Item.Id == itemId);
            if (itemToRemove == null) return;
            if (itemToRemove.Quantity == 1)
            {
                cart.CartItems.Remove(itemToRemove);
            }
            else
            {
                itemToRemove.Quantity -= 1;
            }
            _context.SaveChanges();
        }
        public void BuyItem(CartItem cartItem)
        {

            var customer = _context.Customer.FirstOrDefault(c => c.Seller == cartItem.Item.User);
            if(customer == null)
            {
               
                customer = new Customer();

                customer.BuyerId = cartItem.Cart.User.Id;
                customer.Seller = cartItem.Item.User;
                customer.MoneySpent = cartItem.Quantity * cartItem.Item.Price;

                _context.Customer.Add(customer);
                         
            }
            else
            {
                customer.MoneySpent += cartItem.Quantity * cartItem.Item.Price;
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cart = GetCart();
            cart.CartItems.Clear();
            _context.Cart.Remove(cart);
            _context.SaveChanges();
        }

        public void CheckOut()
        {
            var cart = GetCart();
            foreach (var item in cart.CartItems)
            {
                BuyItem(item);
            }
            ClearCart();

        }

    }
}
