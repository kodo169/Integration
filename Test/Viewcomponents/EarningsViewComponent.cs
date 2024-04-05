using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Test.Data;

namespace Integration.Viewcomponents
{
    public class EarningsViewComponent : ViewComponent
    {
        private readonly HrContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public EarningsViewComponent(HrContext dataSQLServer, MydbContext dataMySQLServer) 
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }

        public IViewComponentResult Invoke()
        {
            var dataHR = _dataSQLServer.Personals.ToList();
            var dataPayroll = _dataMySQLServer.Employees.ToList();
            var dataPr_Pay_Rates= _dataMySQLServer.PayRates.ToList();
            var data = new List<Earnings_ViewModel>();

            if (dataHR.Count == dataPayroll.Count)
            {
                foreach (var hr in dataHR)
                {
                    var prE = dataPayroll.FirstOrDefault(p => p.IdEmployee == hr.EmployeeId &&
                                                                          p.FirstName == hr.FirstName &&
                                                                          p.LastName == hr.LastName);
                    var prPE = dataPr_Pay_Rates.FirstOrDefault(e => e.IdPayRates == prE.PayRatesIdPayRates);
                    if (prE != null && prE != null)
                    {
                        data.Add(new Earnings_ViewModel
                        {
                            FisrtName = hr.FirstName,
                            MiddleInitial =hr.MiddleInitial,
                            LastName = hr.LastName,
                            payRateName = prPE.PayRateName,
                            Gender = hr.Gender,
                            value = prPE.Value,
                            tax = prPE.TaxPercentage,
                            payAmount = prPE.PayAmount,
                            PaidToDate = prE.PaidToDate,
                            PaidLastYear = prE.PaidLastYear,
                        });
                    }
                }
            }
            return View(data);
        }

    }
}
