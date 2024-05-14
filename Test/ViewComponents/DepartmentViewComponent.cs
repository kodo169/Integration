using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Integration.Viewcomponents
{
    public class DepartmentViewComponent : ViewComponent
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;

        public DepartmentViewComponent (HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }

        public IViewComponentResult Invoke()
        {
            var data = new List<Department_ViewModel>();
            var dataJobHistories = _dataSQLServer.JobHistories.ToList();
            foreach (var item in dataJobHistories)
            {
                var dataDeparment = new Department_ViewModel
                {
                    nameDepartment = item.Department,
                };
                data.Add(dataDeparment);
            }
            return View(data);
        }
    }
}
