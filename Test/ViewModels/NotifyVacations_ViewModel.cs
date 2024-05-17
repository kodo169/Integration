namespace Integration.ViewModels
{
    public class NotifyVacations_ViewModel
    {
        public string? id { get; set; }
        public string? FisrtName { get; set; }
        public string? MiddleInitial { get; set; }
        public string? LastName { get; set; }      
        public int Actualday { get; set; }
        public DateOnly? Year { get; set; }
        public int Month { get; set; }
        public int Total { get; set; }
    }
}
