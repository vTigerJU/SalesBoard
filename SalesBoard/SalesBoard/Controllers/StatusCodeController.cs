using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace SalesBoard.Controllers
{
    public class StatusCodeController : Controller
    {
        public IActionResult Index(int code)
        {
            string reasonPhrase = ReasonPhrases.GetReasonPhrase(code);
            reasonPhrase = code.ToString() + " : " + reasonPhrase;
            ViewBag.ReasonPhrase = reasonPhrase;
            return View();
        }
    }
}
