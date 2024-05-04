using Integration.Data;
using Integration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration.Controllers
{
    public class managerPersonalController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public managerPersonalController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }

        public IActionResult Index()
        {
            var inforStaffHRM = _dataSQLServer.Personals
                .Include(p => p.Employments)
                .Where(p => p.Employments.Any(e => e.PersonalId == p.PersonalId))
                .ToList();
            return View(inforStaffHRM);
        }

    }
}
