using Integration.Data;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Integration.ViewModels
{
    public class Vacations_ViewModel
    {
        //tạo các đối tượng tại đây
        //public decimal Id { get; set; }

        public int? idPayRate { get; set; }
        public short? ShareholderStatus { get; set; }
        public string? Gender { get; set; }
        public string? Ethnicity { get; set; }
        public decimal? VacationDays { get; set; }

        public decimal? MonthWorking { get; set; }
        public DateOnly? YearWorking { get; set; }

        public string? EmploymentStatus { get; set; }

        //public virtual Integration.Models.PayRate PayRatesIdPayRatesNavigation { get; set; } = null!;
    }
}