using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration.Controllers
{
    public class VacationController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public VacationController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IActionResult Index()
        {
            var dataHR = _dataSQLServer.Employments
                .Include(p => p.Personal)
                .Include(p => p.EmploymentWorkingTimes)
                .ToList();
            var data = new List<Vacations_ViewModel>();

            foreach (var item in dataHR)
            {
                var dataE = _dataSQLServer.Employments.FirstOrDefault(p => p.PersonalId == item.PersonalId);
                var dataVC = _dataSQLServer.EmploymentWorkingTimes.FirstOrDefault(P => P.EmploymentId == item.EmploymentId);
                if (dataVC == null) continue;
                if (dataE == null) continue;
                if (item.Personal == null) continue;
                var vacationsViewModel = new Vacations_ViewModel
                {
                    
                    ShareholderStatus = item.Personal.ShareholderStatus,
                    Gender = item.Personal.CurrentGender,
                    Ethnicity = item.Personal.Ethnicity,
                    VacationDays = dataVC.TotalNumberVacationWorkingDaysPerMonth,
                    YearWorking = dataVC.YearWorking,
                    MonthWorking = dataVC.MonthWorking,
                    EmploymentStatus = item.EmploymentStatus,
                };
                data.Add(vacationsViewModel);
            }
            return View(data);
        }
    }
}
