using Integration.Data;
using Integration.Models;
using Integration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration.ViewComponents
{
    public class PayRatesViewComponent : ViewComponent
    {
        private readonly HrmContext _dataSQLServer;
        private readonly MydbContext _dataMySQLServer;

        public PayRatesViewComponent(HrmContext dataSQLServer, MydbContext dataMySQLServer)
        {
            _dataSQLServer = dataSQLServer;
            _dataMySQLServer = dataMySQLServer;
        }
        public IViewComponentResult Invoke(int? id, string? namePayrates)
        {
            var data = new List<PayRatesViewModel>();
            var payrates = _dataMySQLServer.PayRates.ToList();
            foreach (var item in payrates)
            {
                var dataname = new PayRatesViewModel
                {
                    IdPayRates = item.IdPayRates,
                    namePayRates = item.PayRateName,
                    defaultNamePayRates = namePayrates,
                    idPayRatesDefaut = id,
                };
                data.Add(dataname);
            }
            return View(data);
        }
    }
}
