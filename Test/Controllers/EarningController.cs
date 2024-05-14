using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

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
        public IActionResult index(string? nameDepartment)
        {
            if (nameDepartment == null) nameDepartment = "Kodo";
            var dataHR = _dataSQLServer.Employments
                .Include(p => p.Personal)
                .Include(p => p.JobHistories)
                .ToList();
            var dataPR = _dataMySQLServer.Employees
                .ToList();
            var data = new List<Earnings_ViewModel>();
            foreach (var item in dataHR)
            {
                foreach (var jobHistory in item.JobHistories)
                {
                    if (jobHistory.Department == nameDepartment)
                    {
                        var infor = dataPR.FirstOrDefault(p => p.EmployeeNumber.ToString() == item.EmploymentCode);
                        if (infor == null) continue;
                        var inforPayroll = _dataMySQLServer.PayRates.FirstOrDefault(p => p.IdPayRates == infor.PayRatesIdPayRates);
                        if (inforPayroll == null) continue;
                        var earningsViewModel = new Earnings_ViewModel
                        {
                            nameDepartment = nameDepartment,
                            idPayRate = infor.PayRatesIdPayRates,
                            ShareholderStatus = item.Personal.ShareholderStatus,
                            FisrtName = item.Personal.CurrentFirstName,
                            MiddleInitial = item.Personal.CurrentMiddleName,
                            LastName = item.Personal.CurrentLastName,
                            Gender = item.Personal.CurrentGender,
                            payRateName = inforPayroll.PayRateName,
                            value = inforPayroll.Value,
                            tax = inforPayroll.TaxPercentage,
                            payAmount = inforPayroll.PayAmount,
                            PaidToDate = infor.PaidToDate,
                            PaidLastYear = infor.PaidLastYear,
                            Ethnicity = item.Personal.Ethnicity,
                        };
                        data.Add(earningsViewModel);
                    }
                }
            }
            return View(data);
        }
    }
}
