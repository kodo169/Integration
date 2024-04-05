using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class BenefitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
