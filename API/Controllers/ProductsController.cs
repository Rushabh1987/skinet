using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
        //We don't need this code anymore because of Rpository Pattern we are using
        // private readonly StoreContext context;

        // public ProductsController(StoreContext context) 
        // {
        //     this.context = context;
        // }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            var spec = new ProductSpecification(brand, type, sort);

            var products = await repo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);

            if(await repo.SaveAllAsync()) {
                return CreatedAtAction("GetProduct", new {id = product.Id}, product);
            }
            return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product) {

            if(product.Id != id || !ProductExists(id)) {
                return BadRequest("Cannot update this Product");
            }

            repo.Update(product);

            if(await repo.SaveAllAsync()) {
                return NoContent();
            }

            return BadRequest("Problem Updating the Product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id) {
            var product = await repo.GetByIdAsync(id);

            if(product == null) return NotFound();

            repo.Remove(product);
            
            if(await repo.SaveAllAsync()) {
                return NoContent();
            }

            return BadRequest("Problem Updating the Product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Strings>>> GetBrands() {
            var spec = new BrandListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Strings>>> GetTypes() {
            var spec = new TypeListSpecification();
            return Ok(await repo.ListAsync(spec));
        }
        public bool ProductExists(int id) {
            return repo.Exists(id);
        }

        
    }
}
