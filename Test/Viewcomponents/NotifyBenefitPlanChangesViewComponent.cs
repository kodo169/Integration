using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Integration.Viewcomponents
{
    public class NotifyBenefitPlanChangesViewComponent : ViewComponent
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public NotifyBenefitPlanChangesViewComponent(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IViewComponentResult Invoke()
        {
            var dataHR = _dataSQLServer.Personals.ToList();
            var dataPayroll = _dataMySQLServer.Employees.ToList();
            var dataPr_Pay_Rates = _dataMySQLServer.PayRates.ToList();
            var data = new List<NotifyBenefitPlanChanges_ViewModel>();

            if (dataHR.Count == dataPayroll.Count)
            {
                foreach (var hr in dataHR)
                {
                    var prE = dataPayroll.FirstOrDefault(p => p.IdEmployee == hr.PersonalId &&
                                                                          p.FirstName == hr.CurrentFirstName &&
                                                                          p.LastName == hr.CurrentLastName);
                    var prPE = dataPr_Pay_Rates.FirstOrDefault(e => e.IdPayRates == prE.PayRatesIdPayRates);
                    if (prE != null && prE != null)
                    {
                        data.Add(new NotifyBenefitPlanChanges_ViewModel
                        {
                            //gán các đối tượng tại đây
                        });
                    }
                }
            }
            return View(data);
        }
    }
}
