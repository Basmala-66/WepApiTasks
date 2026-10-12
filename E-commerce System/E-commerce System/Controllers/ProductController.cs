using E_commerce_System.Data;
using E_commerce_System.DTOs;
using E_commerce_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products =  await _context.Products.ToListAsync();

            return Ok(products);
        }



        [HttpGet("Id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(x=>x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
                return NotFound($"product with i ({id}) is not found");

            var response = new GetProductResponse()
            {
                ProductName = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Createproduct(GetProductRequest request)
        {
            var product = new Product()
            {
                Name =request.ProductName,
                Price= request.Price,
                CategoryId = request.CategoryId

            };

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            return Created();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product is null)
                return NotFound("product not found");

            product.Name = request.ProductName;
            product.Price =request.Price;
            product.CategoryId = request.CategoryId;

            _context.Update(product);
          await  _context.SaveChangesAsync();

            return Ok(product);

        }

        [HttpDelete]
        public async Task<IActionResult> Deleteproduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
                return NotFound("product not found");

            _context.Products.Remove(product);
          await  _context.SaveChangesAsync();

            return Ok("product deleted");
        }
    }
}
