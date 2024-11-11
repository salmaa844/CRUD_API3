using crudAPI.Data;
using crudAPI.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace crudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("GetALL")]
        public IActionResult GetAll()
        {
            var products = dbContext.Products.ToList();
            return Ok(products);
        }
        [HttpGet("Details")]
        public IActionResult GetId(int id)
        {

            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                NotFound();
            }
            return Ok(product);
        }

        [HttpPost("create")]
        public IActionResult CreateProduct(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            //  return Created();
            return CreatedAtAction(nameof(CreateProduct), product);
        }
        [HttpPut("update")]
        public IActionResult Update(int id,Product request)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            
                NotFound();

            product.Name = request.Name;
            product.Description = request.Description;
            dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                NotFound();
            }
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return NoContent();


        }
    }
}
