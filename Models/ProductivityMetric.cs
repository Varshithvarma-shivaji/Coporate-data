using System;
using System.Collections.Generic;

namespace varma.Models;

public partial class ProductivityMetric
{
    public int MetricId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? Date { get; set; }

    public int? TasksCompleted { get; set; }

    public double? WorkHours { get; set; }

    public double? ProductivityScore { get; set; }

    public virtual Employee? Employee { get; set; }
}
