using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class EarningController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}
