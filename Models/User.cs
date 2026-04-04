using System;
using System.Collections.Generic;

namespace varma.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Role? Role { get; set; }
}
