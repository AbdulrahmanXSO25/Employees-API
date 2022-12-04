using System;
using System.Collections.Generic;

namespace EmployeesAPI.DTO;

public partial class DepartmentDTO
{
    public string? DepartmentName { get; set; }

    public string DepartmentAbbr { get; set; } = null!;
}
