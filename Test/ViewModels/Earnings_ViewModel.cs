namespace Integration.ViewModels
{
    public class Earnings_ViewModel
    {
        public decimal Id { get; set; }
        public string? FisrtName { get; set;}
        public string? MiddleInitial { get; set; }
        public string? LastName { get; set; }
        public int PayRatesIdPayRates { get; set; }
        public string? payRateName {  get; set; }
        public decimal? value { get; set; }
        public decimal? tax {  get; set; }
        public bool? Gender { get; set; }
        public decimal? payAmount { get; set; }
        public decimal? PaidToDate { get; set; }

        public decimal? PaidLastYear { get; set; }

        public virtual Integration.Models.PayRate PayRatesIdPayRatesNavigation { get; set; } = null!;
    }
}
