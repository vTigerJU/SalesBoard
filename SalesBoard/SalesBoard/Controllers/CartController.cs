using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesBoard.Data;
using SalesBoard.Models;
using SalesBoard.Services;

namespace SalesBoard.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public CartController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
           
        }
        //Cart
        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            ViewBag.totalcost = cart.CartItems.Sum(ci => ci.Item.Price * ci.Quantity);
            if (cart.CartItems.IsNullOrEmpty())
            {
                return RedirectToAction("Index", "Items");
            }
            return View(cart.CartItems);
        }

        // GET: Cart/Remove/1
        //Removes item from cart, default quantity = 1
        public async Task<IActionResult> Remove(int id, int quantity = 1)
        {
            var item = await _context.Item.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            if(_cartService.RemoveItem(id, quantity)) //If item succesfully removed
            {
                item.Quantity += quantity;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        //Get: Cart/Checkout
        public ActionResult Checkout()
        {
            _cartService.CheckOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
