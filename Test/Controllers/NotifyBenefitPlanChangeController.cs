using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration.Controllers
{
    public class NotifyBenefitPlanChangeController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public NotifyBenefitPlanChangeController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IActionResult Index()
        {
            var data = _dataSQLServer.Employments
                .Include(p =>p.Personal)
                .ToList();
            var result = new List<NotifyBenefitPlanChanges_ViewModel>();
            DateOnly nowDate = DateOnly.FromDateTime(DateTime.Now);
            foreach (var item in data)
            {
                if (item.LastReviewDate == null) continue;
                if(item.LastReviewDate.Value.Month == nowDate.Month && (nowDate.Day - item.LastReviewDate.Value.Day) <= 7)
                {
                    var namePersonal = _dataSQLServer.BenefitPlans.Where(p => p.BenefitPlansId == item.Personal.BenefitPlanId).FirstOrDefault();
                    var addDate = new NotifyBenefitPlanChanges_ViewModel
                    {
                        FirstName = item.Personal.CurrentFirstName,
                        LastName = item.Personal.CurrentLastName,
                        MiiderName = item.Personal.CurrentMiddleName,
                        dateChangeBenefit = item.LastReviewDate,
                        nameBenefit = namePersonal.PlanName,
                    };
                    result.Add(addDate);
                }
            }
            return View(result);
        }
    }
}
