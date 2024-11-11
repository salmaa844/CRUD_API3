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
    public class DepartmentsController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        public DepartmentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("GetALL")]
        public IActionResult GetAll()
        {
            var Department = dbContext.Department.Select(

                x => new GetDepartmentDto(){

                Id = x.Id,
                Name = x.Name,

                 }


            );
            return Ok(Department);
        }
        [HttpGet("Details")]
        public IActionResult GetDepartment(int id)
        {
            var Department = dbContext.Department
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (Department == null)
            {
                return NotFound();
            }

           
            var DepartmentDto = Department.Adapt<GetIdDepartment>();

            return Ok(DepartmentDto);
        }
        [HttpPost("create")]
        public IActionResult CreateDepatment(CreateDepartmentDto empdto)
        {
            Department department = new Department()
            { Name = empdto.Name};
           dbContext.Department.Add(department);
         
            dbContext.SaveChanges();
            return Ok();
        }
        [HttpPut("update")]
        public IActionResult Update(int id, UpdateDepartmentDto request)
        {
            var Department = dbContext.Department.Find(id);
            if (Department == null)

                NotFound();

            Department.Name = request.Name;
            Department.Departments = request.Departments;
            dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var Department = dbContext.Department.Find(id);
            if (Department == null)
            {
                NotFound();
            }
            dbContext.Department.Remove(Department);
            dbContext.SaveChanges();
            return NoContent();


        }
    }
}
