using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test.Data;

public partial class Personal
{
    [Key]
    [DisplayName("Employee ID")]
    public decimal EmployeeId { get; set; }

    [Required]
    [DisplayName("Fisrt Name")]
    public string? FirstName { get; set; }
    [Required]
    [DisplayName("Last Name")]
    public string? LastName { get; set; }
    [DisplayName("Middle Initial")]
    public string? MiddleInitial { get; set; }
    [Required]
    [DisplayName("Address1")]
    public string? Address1 { get; set; }
    [DisplayName("Address2")]
    public string? Address2 { get; set; }
    [Required]
    [DisplayName("City")]
    public string? City { get; set; }
    [DisplayName("State")]
    public string? State { get; set; }
    [Required]
    [DisplayName("Zip")]
    public decimal? Zip { get; set; }
    [Required]
    [DisplayName("Email")]
    public string? Email { get; set; }
    [Required]
    [DisplayName("Phone Number")]
    public string? PhoneNumber { get; set; }
    [Required]
    [DisplayName("Social Security Number")]
    public string? SocialSecurityNumber { get; set; }
    [DisplayName("Drivers License")]
    public string? DriversLicense { get; set; }
    [Required]
    [DisplayName("Marital Status")]
    public string? MaritalStatus { get; set; }
    [Required]
    [DisplayName("Gender")]
    public bool? Gender { get; set; }
    [Required]
    [DisplayName("Shareholder Status")]
    public bool ShareholderStatus { get; set; }
    [Required]
    [DisplayName("Benefit Plans")]
    public decimal? BenefitPlans { get; set; }
    [DisplayName("Ethnicity")]
    public string? Ethnicity { get; set; }
    public virtual BenefitPlan? BenefitPlansNavigation { get; set; }
    public virtual EmergencyContact? EmergencyContact { get; set; }
    public virtual Employment? Employment { get; set; }

    public virtual ICollection<JobHistory> JobHistories { get; set; } = new List<JobHistory>();
}
