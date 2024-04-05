using System;
using System.Collections.Generic;

namespace Test.Data;

public partial class Employment
{
    public decimal EmployeeId { get; set; }

    public string? EmploymentStatus { get; set; }

    public DateTime? HireDate { get; set; }

    public string? WorkersCompCode { get; set; }

    public DateTime? TerminationDate { get; set; }

    public DateTime? RehireDate { get; set; }

    public DateTime? LastReviewDate { get; set; }

    public virtual Personal Employee { get; set; } = null!;
}
