using System;
using System.Collections.Generic;

namespace varma.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int? ProjectId { get; set; }

    public int? EmployeeId { get; set; }

    public string? TaskTitle { get; set; }

    public string? Status { get; set; }

    public string? Priority { get; set; }

    public DateOnly? DueDate { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Project? Project { get; set; }
}
