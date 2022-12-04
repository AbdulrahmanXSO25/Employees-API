using System;
using System.Collections.Generic;

namespace EmployeesAPI.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public string DepartmentAbbr { get; set; } = null!;
}
