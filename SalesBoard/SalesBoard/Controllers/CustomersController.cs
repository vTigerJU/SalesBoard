using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesBoard.Data;
using SalesBoard.Models;

namespace SalesBoard.Controllers
{
    [Authorize(Roles = "User")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Customers
        //Displays list of 1 users customers
        public async Task<IActionResult> Index()
        {
            List<UserCustomerViewModel> userCustomerVM = new List<UserCustomerViewModel>();
            var currentUserId = _userManager.GetUserId(User);
            if (currentUserId != null)
            {
                var customers = from c in _context.Customer select c;
                customers = customers.Where(c => c.Seller.Id == currentUserId);
                var cList = await customers.ToListAsync();
                var users = await _context.Users.ToListAsync();
                var newUserCustomer = new UserCustomerViewModel();

                foreach (var c in cList)
                {
                    newUserCustomer.Buyer = users.First(u => u.Id == c.BuyerId);
                    newUserCustomer.Customer = c;
                    userCustomerVM.Add(newUserCustomer);
                }
                return View(userCustomerVM);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
