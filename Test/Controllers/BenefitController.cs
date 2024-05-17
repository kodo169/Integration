using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration.Controllers
{
    public class BenefitController : Controller
    {
        private readonly HrmContext _dataSQLServer;

        public BenefitController(HrmContext dataSQLServer)
        {
            _dataSQLServer = dataSQLServer;
        }
        public IActionResult Index()
        {
            var data = _dataSQLServer.Personals
               .Include(p => p.BenefitPlan)
               .ToList();
            var benefitPlans = _dataSQLServer.BenefitPlans
                .AsEnumerable()  // Switch to client evaluation
                .Select(bp => new Benefits_ViewModel
                {
                    BenefitId = bp.BenefitPlansId,
                    PlanName = bp.PlanName,
                    AverageBenefitNonshareholder = data.Count(p => p.ShareholderStatus == 0 && p.BenefitPlanId == bp.BenefitPlansId) * (double?)bp.Deductable * (double?)bp.PercentageCopay / 100,
                    AverageBenefitshareholder = data.Count(p => p.ShareholderStatus == 1 && p.BenefitPlanId == bp.BenefitPlansId) * (double?)bp.Deductable * (double?)bp.PercentageCopay / 100
                }).Where(bp => bp.AverageBenefitNonshareholder != 0 || bp.AverageBenefitshareholder != 0)
                .ToList();

            return View(benefitPlans);
        }
    }
}