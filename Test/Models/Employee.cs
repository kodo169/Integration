using System;
using System.Collections.Generic;

namespace Integration.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public int EmployeeNumber { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public decimal Ssn { get; set; }

    public string? PayRate { get; set; }

    public int PayRatesIdPayRates { get; set; }

    public int? VacationDays { get; set; }

    public decimal? PaidToDate { get; set; }

    public decimal? PaidLastYear { get; set; }

    public virtual PayRate PayRatesIdPayRatesNavigation { get; set; } = null!;
}
