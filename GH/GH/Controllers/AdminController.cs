using Microsoft.AspNetCore.Mvc;

namespace GH.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
