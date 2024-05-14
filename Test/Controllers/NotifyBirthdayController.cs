using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Integration.Controllers
{
    public class NotifyBirthdayController : Controller
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;
        public NotifyBirthdayController(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IActionResult Index()
        {
            var dataHRPersonal = _dataSQLServer.Personals.ToList();
            var data = new List<NotifyBirthdays_ViewModel>();
                foreach (var hrP in dataHRPersonal)
                {
                        data.Add(new NotifyBirthdays_ViewModel
                        {
                            FirstName = hrP.CurrentFirstName,
                            LastName = hrP.CurrentLastName,
                            BirthDay  = hrP.BirthDate
                        });
                }
            return View(data);
        }
    }
}
