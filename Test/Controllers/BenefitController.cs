using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class BenefitController : Controller
    {
        private readonly HrmContext _dataSQLServer;

        public BenefitController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
        }
        public IActionResult Index()
        {

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
                        data.Add(new Benefits_ViewModel
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
