using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration.Controllers
{
    public class HiringAnniversaryController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public HiringAnniversaryController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IActionResult Index()
        {
            var dataHREmployment = _dataSQLServer.Employments
                .Include(p => p.Personal)
                .ToList();
                
            var data = new List<HiringAnniversarys_ViewModel>();
                foreach (var item in dataHREmployment)
                {
                    if (item.Personal == null) continue;
                else
                {
                    data.Add(new HiringAnniversarys_ViewModel
                    {
                        FisrtName = item.Personal.CurrentFirstName,
                        MiddleInitial = item.Personal.CurrentMiddleName,
                        LastName = item.Personal.CurrentLastName,
                        HireDate = item.HireDateForWorking
                    });
                }
            }
            return View(data);
        }
    }
}
