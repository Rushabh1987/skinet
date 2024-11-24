using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repo) : ControllerBase
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
            return Ok(await repo.GetProductsAsync(brand, type, sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.AddProduct(product);

            if(await repo.SaveChangesAsync()) {
                return CreatedAtAction("GetProduct", new {id = product.Id}, product);
            }
            return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product) {

            if(product.Id != id || !ProductExists(id)) {
                return BadRequest("Cannot update this Product");
            }

            repo.UpdateProduct(product);

            if(await repo.SaveChangesAsync()) {
                return NoContent();
            }

            return BadRequest("Problem Updating the Product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id) {
            var product = await repo.GetProductByIdAsync(id);

            if(product == null) return NotFound();

            repo.DeleteProduct(product);
            
            if(await repo.SaveChangesAsync()) {
                return NoContent();
            }

            return BadRequest("Problem Updating the Product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Strings>>> GetBrands() {
            return Ok(await repo.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Strings>>> GetTypes() {
            return Ok(await repo.GetTypesAsync());
        }
        public bool ProductExists(int id) {
            return repo.ProductExists(id);
        }

        
    }
}
