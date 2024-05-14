using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Integration.ViewModels
{
    public class informationEmployee
    {
        public int Id { get; set; } 
        public string? CurrentFirstName { get; set; }
        public string? CurrentLastName { get; set; }

        public string? CurrentMiddleName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public string? DriversLicense { get; set; }
        public string? CurrentAddress1 { get; set; }

        public string? CurrentAddress2 { get; set; }

        public string? CurrentCity { get; set; }

        public string? CurrentCountry { get; set; }

        public decimal? CurrentZip { get; set; }

        public string? CurrentGender { get; set; }

        public string? CurrentPhoneNumber { get; set; }

        public string? CurrentPersonalEmail { get; set; }

        public string? CurrentMaritalStatus { get; set; }

        public string? Ethnicity { get; set; }

        public short? ShareholderStatus { get; set; }

        public decimal? BenefitPlanId { get; set; }
        public string? BenefitPlanName { get; set; }
        public decimal Ssn { get; set; }

        public string? PayRate { get; set; }

        public string? PayRatesName { get; set; }
        public int PayRatesIdPayRates { get; set; }

        public int? VacationDays { get; set; }

        public decimal? PaidToDate { get; set; }

        public decimal? PaidLastYear { get; set; }
        public int EmployeeNumber { get; set; }

        public DateOnly? TerminationDate { get; set; }
        public DateOnly? hireDateForWorking { get; set; }

        public decimal? NumberDaysRequirementOfWorkingPerMonth { get; set; }
    }
}
