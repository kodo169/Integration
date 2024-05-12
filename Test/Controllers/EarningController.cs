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
        public IActionResult index()
        {
            var dataHR = _dataSQLServer.Employments
                .Include(p => p.Personal)
                .ToList();
            var dataJobHistories = _dataSQLServer.JobHistories
                .Include(p => p.JobHistoryId);
            var dataPR = _dataMySQLServer.Employees.ToList();
            var data = new List<Earnings_ViewModel>();
            // lấy dữ liệu department
            var department = _dataSQLServer.JobHistories.ToList();
            List<string> checkdepartment = new List<string>();
            foreach (var item1 in department)
            {
                if (item1.Department == null) continue;
                checkdepartment.Add(item1.Department);
            }
            foreach (var item in dataPR)
            {
                var infor = dataHR.FirstOrDefault(p => p.EmploymentId == item.IdEmployee &&
                                                         p.EmploymentCode == item.EmployeeNumber.ToString());
                if (infor == null) continue;
                var inforPayroll =_dataMySQLServer.PayRates.FirstOrDefault(p => p.IdPayRates == item.PayRatesIdPayRates);
                if(inforPayroll == null) continue;
 
                var earningsViewModel = new Earnings_ViewModel
                {
                    Department = checkdepartment.ToArray(),
                    idPayRate = item.PayRatesIdPayRates,
                    ShareholderStatus = infor.Personal.ShareholderStatus,
                    FisrtName = infor.Personal.CurrentFirstName,
                    MiddleInitial = infor.Personal.CurrentMiddleName,
                    LastName = infor.Personal.CurrentLastName,
                    Gender = infor.Personal.CurrentGender,
                    payRateName = inforPayroll.PayRateName,
                    value = inforPayroll.Value,
                    tax = inforPayroll.TaxPercentage,
                    payAmount = inforPayroll.PayAmount,
                    PaidToDate = item.PaidToDate,
                    PaidLastYear = item.PaidLastYear,
                    Ethnicity = infor.Personal.Ethnicity,
                };
                data.Add(earningsViewModel);
            }
            return View(data);
        }
        
    }
}
