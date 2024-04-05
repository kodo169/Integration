using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Test.Data;
using System.Linq;

namespace Integration.Viewcomponents
{
    public class HiringAnniversarysViewComponent : ViewComponent
    {
        private readonly HrContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public HiringAnniversarysViewComponent(HrContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }

        public IViewComponentResult Invoke()
        {
            var dataHRPersonal = _dataSQLServer.Personals.ToList();
            var dataHREmployment = _dataSQLServer.Employments.ToList();
            var dataPayroll = _dataMySQLServer.Employees.ToList();
            var data = new List<HiringAnniversarys_ViewModel>();

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
                        data.Add(new HiringAnniversarys_ViewModel
                        {
                            FisrtName = hrP.FirstName,
                            MiddleInitial = hrP.MiddleInitial,
                            LastName = hrP.LastName,
                            HireDate = hrE.HireDate
                        });
                    }
                }
            }
            return View(data);
        }
    }
}
