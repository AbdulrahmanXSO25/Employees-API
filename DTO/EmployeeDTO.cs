using System;
using System.Collections.Generic;

namespace EmployeesAPI.DTO;

public partial class EmployeeDTO
{
    public string EmployeeNameArabic { get; set; } = null!;

    public string EmployeeNameEnglish { get; set; } = null!;

    public DateTime Dob { get; set; }

    public DateTime HiringDate { get; set; }

    public decimal Salary { get; set; }

    public int? CityId { get; set; }

    public int? DepartmentId { get; set; }
}
