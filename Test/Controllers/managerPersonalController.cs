﻿using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var dataHR = _dataSQLServer.Personals.ToList();
            var dataPR = _dataMySQLServer.Employees.ToList();
            var data = new List<managerPersonal_ViewModel>();
            foreach (var item in dataPR)
            {
                var infor = dataHR.FirstOrDefault(p => p.PersonalId == item.IdEmployee &&
                                          p.CurrentFirstName == item.FirstName &&
                                          p.CurrentLastName == item.LastName);
                if (infor == null) continue;
                var informationPersonal = new managerPersonal_ViewModel
                {
                    FisrtName = item.FirstName,
                    Id = infor.PersonalId,
                    LastName = infor.CurrentLastName,
                    MiddleName = infor.CurrentMiddleName,
                    Email = infor.CurrentPersonalEmail,
                    Country = infor.CurrentCountry,
                    Shareholder = infor.ShareholderStatus,
                };
                data.Add(informationPersonal);
            }
            return View(data);
        }
        public IActionResult edit (int? id)
        {
            var dataHR = _dataSQLServer.Personals.Where(p =>p.PersonalId == id).FirstOrDefault();
            var dataPR = _dataMySQLServer.Employees.Where(p => p.IdEmployee == id).FirstOrDefault();
            if (dataHR == null || dataPR == null) { return Redirect("/404"); }
            else
            {
                if (dataHR.PersonalId == dataPR.IdEmployee && dataHR.CurrentFirstName == dataPR.FirstName && dataHR.CurrentLastName == dataPR.LastName)
                {
                    var data = new informationEmployee
                    {
                        PersonalId = dataHR.PersonalId,
                        CurrentFirstName = dataHR.CurrentFirstName,
                        CurrentLastName = dataHR.CurrentLastName,
                        CurrentMiddleName = dataHR.CurrentMiddleName,
                        BirthDate = dataHR.BirthDate,
                        SocialSecurityNumber = dataHR.SocialSecurityNumber,
                        DriversLicense = dataHR.DriversLicense,
                        CurrentAddress1 = dataHR.CurrentAddress1,
                        CurrentAddress2 = dataHR.CurrentAddress2,
                        CurrentCity = dataHR.CurrentCity,
                        CurrentCountry = dataHR.CurrentCountry,
                        CurrentZip = dataHR.CurrentZip,
                        CurrentGender = dataHR.CurrentGender,
                        CurrentPhoneNumber = dataHR.CurrentPhoneNumber,
                        CurrentPersonalEmail = dataHR.CurrentPersonalEmail,
                        CurrentMaritalStatus = dataHR.CurrentMaritalStatus,
                        Ethnicity = dataHR.Ethnicity,
                        ShareholderStatus = dataHR.ShareholderStatus,
                        BenefitPlanId = dataHR.BenefitPlanId,
                        Ssn = dataPR.Ssn,
                        PayRate = dataPR.PayRate,
                        PayRatesIdPayRates = dataPR.PayRatesIdPayRates,
                        VacationDays = dataPR.VacationDays,
                        PaidToDate = dataPR.PaidToDate,
                        PaidLastYear = dataPR.PaidLastYear,
                        EmployeeNumber = dataPR.EmployeeNumber,
                    };
                    return View(data);
                }
                return Redirect("/404");
            }
        }
        [HttpPost]
        public IActionResult changeInformation(informationEmployee model)
        {
            var dataHR = _dataSQLServer.Personals.Where(p => p.PersonalId == model.PersonalId).FirstOrDefault();
            var dataPR = _dataMySQLServer.Employees.Where(p => p.IdEmployee == model.PersonalId).FirstOrDefault();
            if(dataHR == null || dataPR == null) { return View(); }
            else
            {
                if(dataHR.PersonalId == dataPR.IdEmployee && dataHR.CurrentFirstName == dataPR.FirstName && dataHR.CurrentLastName == dataPR.LastName)
                {
                    dataHR.CurrentFirstName = model.CurrentFirstName;
                    dataHR.CurrentLastName  = model.CurrentLastName;
                    dataHR.CurrentMiddleName = model.CurrentMiddleName;
                    dataHR.BirthDate = model.BirthDate;
                    dataHR.SocialSecurityNumber = model.SocialSecurityNumber;
                    dataHR.DriversLicense = model.DriversLicense;
                    dataHR.CurrentAddress1 = model.CurrentAddress1;
                    dataHR.CurrentAddress2 = model.CurrentAddress2;
                    dataHR.CurrentCity = model.CurrentCity;
                    dataHR.CurrentCountry = model.CurrentCountry;
                    dataHR.CurrentZip = model.CurrentZip;
                    dataHR.CurrentGender = model.CurrentGender;
                    dataHR.CurrentPhoneNumber = model.CurrentPhoneNumber;
                    dataHR.CurrentPersonalEmail = model.CurrentPersonalEmail;
                    dataHR.CurrentMaritalStatus = model.CurrentMaritalStatus;
                    dataHR.Ethnicity = model.Ethnicity;
                    dataHR.ShareholderStatus = model.ShareholderStatus;
                    dataHR.BenefitPlanId = model.BenefitPlanId;
                    dataPR.Ssn = model.Ssn;
                    dataPR.PayRate = model.PayRate;
                    dataPR.PayRatesIdPayRates = model.PayRatesIdPayRates;
                    dataPR.VacationDays = model.VacationDays;
                    dataPR.PaidToDate = model.PaidToDate;
                    dataPR.PaidLastYear = model.PaidLastYear;
                    dataPR.LastName = model.CurrentLastName;
                    dataPR.FirstName = model.CurrentFirstName;
                    dataPR.EmployeeNumber = model.EmployeeNumber;
                    try
                    {
                        _dataSQLServer.SaveChanges();
                        _dataMySQLServer.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "An error occurred while deleting data.";
                        return Redirect("/404");
                    }
                }
            }
            return RedirectToAction("edit",new {id = model.PersonalId });
        }

        public IActionResult delete(int? id)
        {
            var dataHR = _dataSQLServer.Personals.Where(p => p.PersonalId == id).FirstOrDefault();
            var dataPR = _dataMySQLServer.Employees.Where(p => p.IdEmployee == id).FirstOrDefault();
            var dataHRe =_dataSQLServer.Employments.Where(p => p.PersonalId == id).ToList();
            if (dataHR != null && dataPR != null && dataHRe!= null)
            {
                foreach (var employment in dataHRe)
                {
                    _dataSQLServer.Employments.Remove(employment);
                }
                _dataSQLServer.Personals.Remove(dataHR);
                _dataMySQLServer.Employees.Remove(dataPR);
                try
                {
                    _dataSQLServer.SaveChanges();
                    _dataMySQLServer.SaveChanges();
                }
                catch (Exception ex)
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

        public IActionResult addEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult actionAddEmployee(informationEmployee model) 
        {
            var newPersonalHRM = new Personal();
            var newEmployeePR = new Employee();
            if(model != null)
            {
                newPersonalHRM.PersonalId = model.PersonalId;
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
                newEmployeePR.IdEmployee = (Convert.ToInt32(model.PersonalId));
                newEmployeePR.Ssn = model.Ssn;
                newEmployeePR.PayRate = model.PayRate;
                newEmployeePR.PayRatesIdPayRates = model.PayRatesIdPayRates;
                newEmployeePR.VacationDays = model.VacationDays;
                newEmployeePR.PaidToDate = model.PaidToDate;
                newEmployeePR.PaidLastYear = model.PaidLastYear;
                newEmployeePR.LastName = model.CurrentLastName;
                newEmployeePR.FirstName = model.CurrentFirstName;
                newEmployeePR.EmployeeNumber = model.EmployeeNumber;
                _dataSQLServer.Personals.Add(newPersonalHRM);
                _dataMySQLServer.Employees.Add(newEmployeePR);
                try
                {
                    _dataSQLServer.SaveChanges();
                    _dataMySQLServer.SaveChanges();
                }
                catch (Exception ex)
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
