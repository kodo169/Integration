using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class NotifyBirthdayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
