using System;
using System.Collections.Generic;

namespace Integration.Data;

public partial class EmploymentWorkingTime
{
    public decimal EmploymentWorkingTimeId { get; set; }

    public decimal? EmploymentId { get; set; }

    public DateOnly? YearWorking { get; set; }

    public decimal? MonthWorking { get; set; }

    public decimal? NumberDaysActualOfWorkingPerMonth { get; set; }

    public decimal? TotalNumberVacationWorkingDaysPerMonth { get; set; }

    public virtual Employment? Employment { get; set; }
}
