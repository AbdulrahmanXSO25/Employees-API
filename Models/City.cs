using System;
using System.Collections.Generic;

namespace EmployeesAPI.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? CityNameArabic { get; set; }

    public string CityNameEnglish { get; set; } = null!;
}
