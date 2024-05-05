using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class NotifyVacationController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public NotifyVacationController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IActionResult Index()
        {
            var dataHRPersonal = _dataSQLServer.Personals.ToList();
            var dataHREmployment = _dataSQLServer.Employments.ToList();
            var dataPayroll = _dataMySQLServer.Employees.ToList();
            var data = new List<NotifyVacations_ViewModel>();

            if (dataHRPersonal.Count == dataPayroll.Count)
            {
                foreach (var hrP in dataHRPersonal)
                {
                    var prE = dataPayroll.FirstOrDefault(p => p.IdEmployee == hrP.PersonalId &&
                                                                          p.FirstName == hrP.CurrentFirstName &&
                                                                          p.LastName == hrP.CurrentLastName);
                    var hrE = dataHREmployment.FirstOrDefault(e => e.PersonalId == hrP.PersonalId);
                    if (prE != null && hrE != null)
                    {
                        data.Add(new NotifyVacations_ViewModel
                        {
                            //gán giá trị cho các đối tượng
                        });
                    }
                }
            }
            return View(data);
        }
    }
}
