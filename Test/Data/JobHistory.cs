using System;
using System.Collections.Generic;

namespace Test.Data;

public partial class JobHistory
{
    public decimal Id { get; set; }

    public decimal EmployeeId { get; set; }

    public string? Department { get; set; }

    public string? Division { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? JobTitle { get; set; }

    public decimal? Supervisor { get; set; }

    public string? JobCategory { get; set; }

    public string? Location { get; set; }

    public decimal? DepartmenCode { get; set; }

    public decimal? SalaryType { get; set; }

    public string? PayPeriod { get; set; }

    public decimal? HoursPerWeek { get; set; }

    public bool? HazardousTraining { get; set; }

    public virtual Personal Employee { get; set; } = null!;
}
