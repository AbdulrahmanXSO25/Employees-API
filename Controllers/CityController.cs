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
    public class CityController : ControllerBase
    {
        private readonly EmployeesDb224Context _context;

        public CityController(EmployeesDb224Context context)
        {
            _context = context;
        }

        // GET: api/City
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            return await _context.Cities.ToListAsync();
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/City/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, [FromBody] CityDTO dto)
        {
            var city = await _context.Cities.SingleOrDefaultAsync(m => m.CityId == id);

            if (city == null) { return NotFound(value: $"NO CITY WAS FOUND WITH ID {id}"); }

            city.CityNameEnglish = dto.CityNameEnglish;
            city.CityNameArabic = dto.CityNameArabic;

            await _context.SaveChangesAsync();

            return Ok(city);
        }
            // POST: api/City
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCity([FromBody] CityDTO dto)
        {
          if (dto == null)
          {
              return BadRequest();
          }

            var city = new City()
            {
                CityNameArabic = dto.CityNameArabic,
                CityNameEnglish = dto.CityNameEnglish
            };

            await _context.Cities.AddAsync(city);

            var returnedValue = _context.SaveChanges() > 0;

            return Ok(new Result<bool>
            {
                Data = returnedValue,
                Success = true
            });
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = _context.Cities.SingleOrDefault(m =>  m.CityId == id);
            if (city == null)
            {
                return BadRequest();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
    public class Result
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string[] Errors { get; set; }

        public Result() { }

        public Result(bool success)
        {
            Success = success;
        }

        public static Result Successed
        {
            get { return new Result(true); }
        }

    }
    public class Result<T> : Result
    {
        public new T Data { get; set; }

    }
}
