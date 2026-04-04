using System;
using System.Collections.Generic;

namespace varma.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? DepartmentId { get; set; }

    public DateOnly? JoinDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<ProductivityMetric> ProductivityMetrics { get; set; } = new List<ProductivityMetric>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual User? User { get; set; }
}
