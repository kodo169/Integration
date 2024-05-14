using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration.Controllers
{
    public class NotifyVacationController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public NotifyVacationController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IActionResult Index()
        {
            var dataHREmployment = _dataSQLServer.EmploymentWorkingTimes
                .Include(p => p.Employment.Personal)
                .ToList();

            var data = new List<NotifyVacations_ViewModel>();
            foreach (var item in dataHREmployment)
            {
                if (item.Employment.Personal == null) continue;
                else
                {
                    data.Add(new NotifyVacations_ViewModel
                    {
                        FisrtName = item.Employment.Personal.CurrentFirstName,
                        MiddleInitial = item.Employment.Personal.CurrentMiddleName,
                        LastName = item.Employment.Personal.CurrentLastName,
                        Actualday = Convert.ToInt32(item.NumberDaysActualOfWorkingPerMonth),
                        Year = item.YearWorking,
                        Month = Convert.ToInt32(item.MonthWorking),
                        Total = Convert.ToInt32(item.TotalNumberVacationWorkingDaysPerMonth),
                    });
                }
            }
            return View(data);
        }
    }
}
