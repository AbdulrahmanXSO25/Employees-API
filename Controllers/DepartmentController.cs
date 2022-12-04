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
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeesDb224Context _context;

        public DepartmentController(EmployeesDb224Context context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
          if (_context.Departments == null)
          {
              return NotFound();
          }
            return await _context.Departments.ToListAsync();
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
          if (_context.Departments == null)
          {
              return NotFound();
          }
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, [FromBody] DepartmentDTO dto)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(m => m.DepartmentId == id);

            if (department == null) { return NotFound(value: $"NO DEPARTMENT WAS FOUND WITH ID {id}"); }

            department.DepartmentName = dto.DepartmentName;
            department.DepartmentAbbr = dto.DepartmentAbbr;

            await _context.SaveChangesAsync();

            return Ok(department);
        }

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody] DepartmentDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var department = new Department()
            {
                DepartmentName = dto.DepartmentName,
                DepartmentAbbr = dto.DepartmentAbbr
            };

            await _context.Departments.AddAsync(department);

            var returnedValue = _context.SaveChanges() > 0;

            return Ok(new Result<bool>
            {
                Data = returnedValue,
                Success = true
            });
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var employee = _context.Employees.SingleOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return BadRequest();
            }


            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return (_context.Departments?.Any(e => e.DepartmentId == id)).GetValueOrDefault();
        }
    }
}
