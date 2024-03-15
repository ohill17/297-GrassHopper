using Microsoft.AspNetCore.Mvc;

namespace GH.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
