using System;
using System.Collections.Generic;

namespace EmployeesAPI.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeNameArabic { get; set; } = null!;

    public string EmployeeNameEnglish { get; set; } = null!;

    public DateTime Dob { get; set; }

    public DateTime HiringDate { get; set; }

    public decimal Salary { get; set; }

    public int? CityId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual City? City { get; set; }

    public virtual Department? Department { get; set; }
}
