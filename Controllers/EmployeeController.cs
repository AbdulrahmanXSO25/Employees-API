using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Models;
using EmployeesAPI.DTO;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeesDb224Context _context;

        public EmployeeController(EmployeesDb224Context context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
           var employees = await _context.Employees.Include(c => c.City).Include(d => d.Department).ToListAsync();

            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees.Include(c => c.City).Include(d => d.Department).SingleOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null) { return NotFound(value: $"NO EMPLOYEE WAS FOUND WITH ID {id}"); };

            return Ok(employee);
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, [FromBody] EmployeeDTO dto)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null) { return NotFound(value: $"NO Employee WAS FOUND WITH ID {id}"); }

            employee.EmployeeNameEnglish = dto.EmployeeNameEnglish;
            employee.EmployeeNameArabic = dto.EmployeeNameArabic;
            employee.Dob = dto.Dob;
            employee.HiringDate = dto.HiringDate;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;
            employee.CityId = dto.CityId;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] EmployeeDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var employee = new Employee()
            {
                EmployeeNameEnglish = dto.EmployeeNameEnglish,
                EmployeeNameArabic = dto.EmployeeNameArabic,
                Dob = dto.Dob,
                HiringDate = dto.HiringDate,
                Salary = dto.Salary,
                CityId = dto.CityId,
                DepartmentId = dto.DepartmentId
            };

            await _context.Employees.AddAsync(employee);

            var returnedValue = _context.SaveChanges() > 0;

            return Ok(new Result<bool>
            {
                Data = returnedValue,
                Success = true
            });
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }

}