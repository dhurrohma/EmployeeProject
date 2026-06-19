using Microsoft.AspNetCore.Mvc;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        public readonly AppDbContext ctx;

        public EmployeeController(AppDbContext ctx) 
        { 
            this.ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var empList = ctx.employees.ToList();
                return Ok(new { data = empList });
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nomor/{nomor}")]
        public async Task<IActionResult> GetById(int nomor)
        {
            try
            {
                var emp = ctx.employees.Find(nomor);

                if (emp == null) return NotFound(new { message = "Employee not found" });

                return Ok(new { data = emp });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nama/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var emp = ctx.employees.FirstOrDefault(emp => emp.Name == name);

                if (emp == null) return NotFound(new { message = "Employee not found" });

                return Ok(new { data = emp });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("kemandoran/{kemandoran}")]
        public async Task<IActionResult> GetByKemandoran(string kemandoran)
        {
            try
            {
                var emp = ctx.employees.FirstOrDefault(emp => emp.Kemandoran == kemandoran);

                if (emp == null) return NotFound(new { message = "Employee not found" });

                return Ok(new { data = emp });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> create([FromBody] Employee employee)
        {
            try
            {
                ctx.employees.Add(employee);
                await ctx.SaveChangesAsync();
                return Ok(new { message = "Employee Created Successfully", data = employee });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpPut("update/{nomor}")]
        public async Task<IActionResult> update(int nomor, [FromBody] Employee empUpdate)
        {
            try
            {
                var emp = ctx.employees.Find(nomor);
                if (emp == null) return NotFound(new { message = "Employee not found" });

                emp.Name = empUpdate.Name;
                emp.Kemandoran = empUpdate.Kemandoran;
                ctx.SaveChanges();

                return Ok(new { message = "Employee Updated Successfully", data = emp });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{nomor}")]
        public async Task<IActionResult> delete(int nomor)
        {
            try
            {
                var emp = ctx.employees.Find(nomor);
                if (emp == null) return NotFound(new { message = "Employee Not Found" });

                ctx.Remove(emp);

                return Ok(new { message = "Employee Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
