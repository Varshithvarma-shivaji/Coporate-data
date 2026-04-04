using System;
using System.Collections.Generic;

namespace varma.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? Salary1 { get; set; }

    public int? ExperienceYears { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public virtual Employee? Employee { get; set; }
}
