using System;
using System.Collections.Generic;

namespace varma.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public double? WorkHours { get; set; }

    public virtual Employee? Employee { get; set; }
}
