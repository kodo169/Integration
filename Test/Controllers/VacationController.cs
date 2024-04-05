using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class VacationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
