using System;
using System.Collections.Generic;

namespace EmployeesAPI.DTO;

public partial class CityDTO
{
    public string? CityNameArabic { get; set; }

    public string CityNameEnglish { get; set; } = null!;
}
