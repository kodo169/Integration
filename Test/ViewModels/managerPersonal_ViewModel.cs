namespace Integration.ViewModels
{
    public class managerPersonal_ViewModel
    {
        public int id { get; set; }
        public decimal numberEmployee { get; set; }
        public string? FisrtName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string? Country { get; set;}
        public string? Email { get; set;}
        public short? Shareholder { get; set; }

        public string? PlanName { get; set; }

        public decimal? Deductable { get; set; }

        public decimal? PercentageCopay { get; set; }
    }
}
