using crudAPI.Data.Models;
using crudAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using crudAPI.Dtos;
using Mapster;
namespace crudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("GetALL")]
        public IActionResult GetAll()
        {
            var employee = dbContext.Employees.ToList();
            var empdto = employee.Adapt<IEnumerable<GetEmployeeDto>>();
            return Ok(empdto);
        }
        [HttpGet("Details")]
        public IActionResult GetId(int id)
        {

            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                NotFound();
            }
            var employeeDto = employee.Adapt<GetTdEmployeeDto>();

            return Ok(employeeDto);
        }

        [HttpPost("create")]
        public IActionResult CreateEmployee(CreateEmployeeDto empDto)
        {
            var employee = empDto.Adapt<Employee>();
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            //  return Created();
            return Ok();
        }
        [HttpPut("update")]
        public IActionResult Update(int id, UpdateEmployeeDto request)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)

                NotFound();

            request.Adapt(employee);
            dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return NoContent();


        }
    }
}
