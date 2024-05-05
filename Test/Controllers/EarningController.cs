using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class EarningController : Controller
    {
            
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public EarningController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IActionResult index()
        {
            var dataHR = _dataSQLServer.Personals.ToList();
            var dataPayroll = _dataMySQLServer.Employees.ToList();
            var dataPrPayRates = _dataMySQLServer.PayRates.ToList();

            var data = new List<Earnings_ViewModel>();
            if (dataHR.Count != dataPayroll.Count)
            {
                return View(data);
            }
            foreach (var hr in dataHR)
            {
                var prE = dataPayroll.FirstOrDefault(p => p.IdEmployee == hr.PersonalId &&
                                                          p.FirstName == hr.CurrentFirstName &&
                                                          p.LastName == hr.CurrentLastName);
                if (prE == null)
                {
                    continue;
                }
                var prPE = dataPrPayRates.FirstOrDefault(e => e.IdPayRates == prE.PayRatesIdPayRates);
                if (prPE == null)
                {
                    continue;
                }
                var earningsViewModel = new Earnings_ViewModel
                {
                    FisrtName = hr.CurrentFirstName,
                    MiddleInitial = hr.CurrentMiddleName,
                    LastName = hr.CurrentLastName,
                    payRateName = prPE.PayRateName,
                    Gender = hr.CurrentGender,
                    value = prPE.Value,
                    tax = prPE.TaxPercentage,
                    payAmount = prPE.PayAmount,
                    PaidToDate = prE.PaidToDate,
                    PaidLastYear = prE.PaidLastYear,
                    Ethnicity = hr.Ethnicity,
                };

                data.Add(earningsViewModel);
            }
            return View(data);
        }
    }
}
