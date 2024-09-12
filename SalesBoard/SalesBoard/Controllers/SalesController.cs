using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesBoard.Data;
using SalesBoard.Models;
using SalesBoard.Services;

namespace SalesBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var salesStatistcs = _context.SalesStatistics.FirstOrDefault();
            return View(salesStatistcs);
        }
    }
}
