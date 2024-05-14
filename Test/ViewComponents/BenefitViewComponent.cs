using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Integration.ViewComponents
{
    public class BenefitViewComponent : ViewComponent
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;

        public BenefitViewComponent(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        
        public IViewComponentResult Invoke(decimal? id, string? nameBenefit)
        {
            var data = new List<listbenefit_ViewModel>();
            var benefit = _dataSQLServer.BenefitPlans.ToList();
            foreach (var item in benefit)
            {
                var dataname = new listbenefit_ViewModel
                {
                    IdDefault = id,
                    defaultNameBenefit = nameBenefit,
                    Id = item.BenefitPlansId,
                    nameBenefit = item.PlanName,
                };
                data.Add(dataname);
            }
            return View(data);
        }
    }
}
