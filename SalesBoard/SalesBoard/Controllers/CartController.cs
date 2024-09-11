using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesBoard.Data;
using SalesBoard.Models;
using SalesBoard.Services;

namespace SalesBoard.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CartController(ApplicationDbContext context, CartService cartService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _cartService = cartService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
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
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.Item.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _cartService.RemoveItem(id);
            item.Quantity += 1;
            await _context.SaveChangesAsync();

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
