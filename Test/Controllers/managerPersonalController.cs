using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Integration.Controllers
{
    public class managerPersonalController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public managerPersonalController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }

        public IActionResult Index()
        {
            var dataHRM = _dataSQLServer.Employments
                .Include(p => p.Personal)
                .ToList();
            var dataPR = _dataMySQLServer.Employees.ToList();
            var data = new List<managerPersonal_ViewModel>();
            foreach (var item in dataHRM)
            {
                var infor = dataPR.FirstOrDefault(p => p.EmployeeNumber.ToString() == item.EmploymentCode);

                if (infor == null || item.Personal == null) continue;
                var informationPersonal = new managerPersonal_ViewModel
                {
                    id = infor.IdEmployee,
                    FisrtName = infor.FirstName,
                    numberEmployee = infor.EmployeeNumber,
                    LastName = infor.LastName,
                    MiddleName = item.Personal.CurrentMiddleName,
                    Email = item.Personal.CurrentPersonalEmail,
                    Country = item.Personal.CurrentCountry,
                    Shareholder = item.Personal.ShareholderStatus,
                };
                data.Add(informationPersonal);
            }
            return View(data);
        }
        public IActionResult edit (int? numberEmployment)
        {
            var dataHR = _dataSQLServer.Employments
                .Include(p => p.Personal)
                .Where(p =>p.EmploymentCode == numberEmployment.ToString()).FirstOrDefault();
            var dataPR = _dataMySQLServer.Employees.Where(p => p.EmployeeNumber == numberEmployment)
                .FirstOrDefault();
            if (dataHR == null || dataPR == null || dataHR.Personal ==null) { return Redirect("/404"); }
            //add dữ liệu phải cho benefit và payrates
            var databenefit = _dataSQLServer.BenefitPlans.Where(p => p.BenefitPlansId == dataHR.Personal.BenefitPlanId).FirstOrDefault();
            var dataPayRates = _dataMySQLServer.PayRates.Where(p =>p.IdPayRates == dataPR.PayRatesIdPayRates).FirstOrDefault();
            if (databenefit == null || dataPayRates == null) return Redirect("/404");
            else
            {
                var data = new informationEmployee
                {
                    CurrentFirstName = dataHR.Personal.CurrentFirstName,
                    CurrentLastName = dataHR.Personal.CurrentLastName,
                    CurrentMiddleName = dataHR.Personal.CurrentMiddleName,
                    BirthDate = dataHR.Personal.BirthDate,
                    SocialSecurityNumber = dataHR.Personal.SocialSecurityNumber,
                    DriversLicense = dataHR.Personal.DriversLicense,
                    CurrentAddress1 = dataHR.Personal.CurrentAddress1,
                    CurrentAddress2 = dataHR.Personal.CurrentAddress2,
                    CurrentCity = dataHR.Personal.CurrentCity,
                    CurrentCountry = dataHR.Personal.CurrentCountry,
                    CurrentZip = dataHR.Personal.CurrentZip,
                    CurrentGender = dataHR.Personal.CurrentGender,
                    CurrentPhoneNumber = dataHR.Personal.CurrentPhoneNumber,
                    CurrentPersonalEmail = dataHR.Personal.CurrentPersonalEmail,
                    CurrentMaritalStatus = dataHR.Personal.CurrentMaritalStatus,
                    Ethnicity = dataHR.Personal.Ethnicity,
                    ShareholderStatus = dataHR.Personal.ShareholderStatus,
                    BenefitPlanId = dataHR.Personal.BenefitPlanId,
                    BenefitPlanName = databenefit.PlanName,
                    Ssn = dataPR.Ssn,
                    PayRate = dataPR.PayRate,
                    PayRatesIdPayRates = dataPR.PayRatesIdPayRates,
                    VacationDays = dataPR.VacationDays,
                    PaidToDate = dataPR.PaidToDate,
                    PaidLastYear = dataPR.PaidLastYear,
                    EmployeeNumber = dataPR.EmployeeNumber,
                    PayRatesName = dataPayRates.PayRateName,
                    NumberDaysRequirementOfWorkingPerMonth = dataHR.NumberDaysRequirementOfWorkingPerMonth,
                    TerminationDate = dataHR.TerminationDate,
                    hireDateForWorking = dataHR.HireDateForWorking,
                };
                return View(data);
            }
        }
        [HttpPost]
        public IActionResult changeInformation(informationEmployee model)
        {
            var dataHRM = _dataSQLServer.Employments
                .Include(p => p.Personal)
                .Where(p => p.EmploymentCode == model.EmployeeNumber.ToString()).FirstOrDefault();
            var dataPR = _dataMySQLServer.Employees.Where(p => p.EmployeeNumber == model.EmployeeNumber).FirstOrDefault();
            if (dataHRM == null || dataPR == null) { return View(); }
            if(model.PayRatesIdPayRates == 0) model.PayRatesIdPayRates = dataPR.PayRatesIdPayRates;
            else
            {
                if (dataHRM.Personal == null) return View();
                dataHRM.Personal.CurrentFirstName = model.CurrentFirstName;
                dataHRM.Personal.CurrentLastName = model.CurrentLastName;
                dataHRM.Personal.CurrentMiddleName = model.CurrentMiddleName;
                dataHRM.Personal.BirthDate = model.BirthDate;
                dataHRM.Personal.SocialSecurityNumber = model.SocialSecurityNumber;
                dataHRM.Personal.DriversLicense = model.DriversLicense;
                dataHRM.Personal.CurrentAddress1 = model.CurrentAddress1;
                dataHRM.Personal.CurrentAddress2 = model.CurrentAddress2;
                dataHRM.Personal.CurrentCity = model.CurrentCity;
                dataHRM.Personal.CurrentCountry = model.CurrentCountry;
                dataHRM.Personal.CurrentZip = model.CurrentZip;
                dataHRM.Personal.CurrentGender = model.CurrentGender;
                dataHRM.Personal.CurrentPhoneNumber = model.CurrentPhoneNumber;
                dataHRM.Personal.CurrentPersonalEmail = model.CurrentPersonalEmail;
                dataHRM.Personal.CurrentMaritalStatus = model.CurrentMaritalStatus;
                dataHRM.Personal.Ethnicity = model.Ethnicity;
                dataHRM.Personal.ShareholderStatus = model.ShareholderStatus;
                if(model.BenefitPlanId != null) dataHRM.Personal.BenefitPlanId = model.BenefitPlanId;
                var newDataPR = new Employee
                {
                    Ssn = model.Ssn,
                    PayRate = model.PayRate,
                    VacationDays = model.VacationDays,
                    PaidToDate = model.PaidToDate,
                    PaidLastYear = model.PaidLastYear,
                    LastName = model.CurrentLastName,
                    FirstName = model.CurrentFirstName,
                    EmployeeNumber = model.EmployeeNumber,
                    PayRatesIdPayRates = model.PayRatesIdPayRates,
                };
                try
                {
                    _dataSQLServer.SaveChanges();
                    _dataMySQLServer.Remove(dataPR);
                    _dataMySQLServer.Add(newDataPR);
                    _dataMySQLServer.SaveChanges();

                }
                catch
                {
                    ViewBag.ErrorMessage = "An error occurred while deleting data.";
                    return Redirect("/404");
                }
            }
            return RedirectToAction("edit",new { numberEmployment = model.EmployeeNumber });
        }

        public IActionResult delete(int? numberEmployment)
        {
            var dataPR = _dataMySQLServer.Employees.Where(p => p.EmployeeNumber == numberEmployment).FirstOrDefault();
            var dataHRM_E =_dataSQLServer.Employments.Where(p => p.EmploymentCode == numberEmployment.ToString()).FirstOrDefault();
            if (dataPR != null && dataHRM_E != null)
            {
                var dataHRM_P = _dataSQLServer.Personals.Where(P => P.PersonalId == dataHRM_E.PersonalId).FirstOrDefault();
                var dataHRM_JH = _dataSQLServer.JobHistories.Where(P => P.EmploymentId == dataHRM_E.EmploymentId).FirstOrDefault();
                var dataHRM_EWT = _dataSQLServer.EmploymentWorkingTimes.Where(P => P.EmploymentId == dataHRM_E.EmploymentId).FirstOrDefault();
                if (dataHRM_P != null )
                {
                    _dataSQLServer.Personals.Remove(dataHRM_P);
                }
                if (dataHRM_EWT != null)
                {
                    _dataSQLServer.EmploymentWorkingTimes.Remove(dataHRM_EWT);
                }
                if(dataHRM_JH != null)
                {
                    _dataSQLServer.JobHistories.Remove(dataHRM_JH);

                }
                    _dataSQLServer.Employments.Remove(dataHRM_E);
                    _dataMySQLServer.Employees.Remove(dataPR);
                try
                {
                    _dataSQLServer.SaveChanges();
                    _dataMySQLServer.SaveChanges();
                }
                catch
                {
                    TempData["Message"] = "An error occurred while deleting data.";
                    return Redirect("/404");
                }
            }
            else
            {
                TempData["Message"] = "Data does not exist.";
                return Redirect("/404");
            }
            return RedirectToAction("index");
        }
        [Route("/addEmployee")]
        public IActionResult addEmployee()
        {
            return View();
        }

        private int calNumberEployee(int check)
        {
            Random random = new Random();
            int numberEmployee = random.Next(1,10000000);
            var dataPR = _dataMySQLServer.Employees.ToList();
            foreach (var item in dataPR)
            {
                if(numberEmployee == item.EmployeeNumber || check == 0)
                {
                    numberEmployee = calNumberEployee(numberEmployee);
                }
            }
            return numberEmployee;
        }
        [HttpPost]
        public IActionResult actionAddEmployee(informationEmployee model) 
        {
        var newPersonalHRM = new Personal();
        var newEmployememtHRM = new Employment();
        var newEmployeePR = new Employee();
        int id = calNumberEployee(0);
        if (model != null)
        {
            //start add Personal HRM
            newPersonalHRM.PersonalId = id;
            newPersonalHRM.CurrentFirstName = model.CurrentFirstName;
            newPersonalHRM.CurrentLastName = model.CurrentLastName;
            newPersonalHRM.CurrentMiddleName = model.CurrentMiddleName;
            newPersonalHRM.BirthDate = model.BirthDate;
            newPersonalHRM.SocialSecurityNumber = model.SocialSecurityNumber;
            newPersonalHRM.DriversLicense = model.DriversLicense;
            newPersonalHRM.CurrentAddress1 = model.CurrentAddress1;
            newPersonalHRM.CurrentAddress2 = model.CurrentAddress2;
            newPersonalHRM.CurrentCity = model.CurrentCity;
            newPersonalHRM.CurrentCountry = model.CurrentCountry;
            newPersonalHRM.CurrentZip = model.CurrentZip;
            newPersonalHRM.CurrentGender = model.CurrentGender;
            newPersonalHRM.CurrentPhoneNumber = model.CurrentPhoneNumber;
            newPersonalHRM.CurrentPersonalEmail = model.CurrentPersonalEmail;
            newPersonalHRM.CurrentMaritalStatus = model.CurrentMaritalStatus;
            newPersonalHRM.Ethnicity = model.Ethnicity;
            newPersonalHRM.ShareholderStatus = model.ShareholderStatus;
            newPersonalHRM.BenefitPlanId = model.BenefitPlanId;
            //end add personal HRM
            //start addEmployememt 
            newEmployememtHRM.EmploymentId = id;
            newEmployememtHRM.EmploymentCode = id.ToString();
            newEmployememtHRM.PersonalId = id;
            newEmployememtHRM.HireDateForWorking = DateOnly.FromDateTime(DateTime.Now);
            newEmployememtHRM.EmploymentStatus = "Working";
            newEmployememtHRM.TerminationDate = model.TerminationDate;
            newEmployememtHRM.NumberDaysRequirementOfWorkingPerMonth = model.NumberDaysRequirementOfWorkingPerMonth;
            //end addEmployememt
            //start add employee PR
            newEmployeePR.IdEmployee = id;
            newEmployeePR.Ssn = model.Ssn;
            newEmployeePR.PayRate = model.PayRate;
            newEmployeePR.PayRatesIdPayRates = model.PayRatesIdPayRates;// change thành payRateName
            newEmployeePR.VacationDays = model.VacationDays;
            newEmployeePR.PaidToDate = model.PaidToDate;
            newEmployeePR.PaidLastYear = model.PaidLastYear;
            newEmployeePR.LastName = model.CurrentLastName;
            newEmployeePR.FirstName = model.CurrentFirstName;
            newEmployeePR.EmployeeNumber = id;
            //end add add employee PR

            _dataSQLServer.Employments.Add(newEmployememtHRM);
            _dataSQLServer.Personals.Add(newPersonalHRM);
            _dataMySQLServer.Employees.Add(newEmployeePR);
            try
            {
                _dataSQLServer.SaveChanges();
                _dataMySQLServer.SaveChanges();
            }
            catch
            {
                TempData["Message"] = "An error occurred while deleting data.";
                return Redirect("/404");
            }
        }
                else
                {
                    TempData["Message"] = "Data does not exist.";
                    return Redirect("/404");
                }
                return RedirectToAction("index");
        }
    }
}
