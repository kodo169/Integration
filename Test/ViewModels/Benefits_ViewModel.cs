using System.Data.SqlTypes;

namespace Integration.ViewModels
{
    public class Benefits_ViewModel
    {
        //tạo các đối tượng tại đây
        public decimal BenefitId { get; set; }
        public string? PlanName { get; set; }

        public short? ShareholderStatus { get; set; }

        public double? AverageBenefitNonshareholder { get; set;}
        public double? AverageBenefitshareholder { get; set; }
    }
}
