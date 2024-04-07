using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Test.Data;

namespace Integration.Viewcomponents
{
    public class NotifyVacationsViewCompoment : ViewComponent
    {
        private readonly HrContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public NotifyVacationsViewCompoment(HrContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IViewComponentResult Invoke()
        {
            var dataHRPersonal = _dataSQLServer.Personals.ToList();
            var dataHREmployment = _dataSQLServer.Employments.ToList();
            var dataPayroll = _dataMySQLServer.Employees.ToList();
            var data = new List<NotifyVacations_ViewModel>();

            if (dataHRPersonal.Count == dataPayroll.Count)
            {
                foreach (var hrP in dataHRPersonal)
                {
                    var prE = dataPayroll.FirstOrDefault(p => p.IdEmployee == hrP.EmployeeId &&
                                                                          p.FirstName == hrP.FirstName &&
                                                                          p.LastName == hrP.LastName);
                    var hrE = dataHREmployment.FirstOrDefault(e => e.EmployeeId == hrP.EmployeeId);
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
