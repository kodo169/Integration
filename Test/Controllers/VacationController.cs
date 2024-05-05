using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var dataHR = _dataSQLServer.Personals.ToList();
            var dataPayroll = _dataMySQLServer.Employees.ToList();
            var dataPr_Pay_Rates = _dataMySQLServer.PayRates.ToList();
            var data = new List<Vacations_ViewModel>();

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
                        data.Add(new Vacations_ViewModel
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
