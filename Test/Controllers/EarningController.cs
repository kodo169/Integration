using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var dataPR = _dataMySQLServer.Employees.ToList();
            var data = new List<Earnings_ViewModel>();

            foreach (var item in dataPR)
            {
                var infor = dataHR.FirstOrDefault(p => p.PersonalId == item.IdEmployee &&
                                                          p.CurrentFirstName == item.FirstName &&
                                                          p.CurrentLastName == item.LastName);
                if (infor == null) continue;
                var inforPayroll =_dataMySQLServer.PayRates.FirstOrDefault(p => p.IdPayRates == item.PayRatesIdPayRates);
                if(inforPayroll == null) continue;
                var earningsViewModel = new Earnings_ViewModel
                {
                    FisrtName = infor.CurrentFirstName,
                    MiddleInitial = infor.CurrentMiddleName,
                    LastName = infor.CurrentLastName,
                    Gender = infor.CurrentGender,
                    payRateName = inforPayroll.PayRateName,
                    value = inforPayroll.Value,
                    tax = inforPayroll.TaxPercentage,
                    payAmount = inforPayroll.PayAmount,
                    PaidToDate = item.PaidToDate,
                    PaidLastYear = item.PaidLastYear,
                    Ethnicity = infor.Ethnicity,
                };
                data.Add(earningsViewModel);
            }
            return View(data);
        }
        
    }
}
