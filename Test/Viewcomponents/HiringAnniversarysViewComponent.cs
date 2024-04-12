using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Integration.Data;

namespace Integration.Viewcomponents
{
    public class HiringAnniversarysViewComponent : ViewComponent
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public HiringAnniversarysViewComponent(HrmContext dataSQLServer, MydbContext dataMySQLServer)
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
                    var prE = dataPayroll.FirstOrDefault(p => p.IdEmployee == hrP.PersonalId &&
                                                                          p.FirstName == hrP.CurrentFirstName &&
                                                                          p.LastName == hrP.CurrentLastName);
                    var hrE = dataHREmployment.FirstOrDefault(e => e.PersonalId == hrP.PersonalId);
                    if (prE != null && hrE != null)
                    {
                        data.Add(new HiringAnniversarys_ViewModel
                        {
                            FisrtName = hrP.CurrentFirstName,
                            MiddleInitial = hrP.CurrentMiddleName,
                            LastName = hrP.CurrentLastName,
                            HireDate = hrE.HireDateForWorking
                        });
                    }
                }
            }
            return View(data);
        }
    }
}
