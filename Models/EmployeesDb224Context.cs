using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Models;

public partial class EmployeesDb224Context : DbContext
{

    public EmployeesDb224Context(DbContextOptions<EmployeesDb224Context> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

}
