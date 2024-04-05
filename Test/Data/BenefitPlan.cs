using System;
using System.Collections.Generic;

namespace Test.Data;

public partial class BenefitPlan
{
    public decimal BenefitPlanId { get; set; }

    public string? PlanName { get; set; }

    public decimal? Deductable { get; set; }

    public int? PercentageCoPay { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
