using E_commerce_System.CateroryDTOs;
using E_commerce_System.Data;
using E_commerce_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return Ok(categories);
        }



        [HttpGet("getCategory")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
                return NotFound($"category with i ({id}) is not found");

            var response = new GetCustomerResponse()
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Createproduct(CreateCustomerRequest request)
        {
            var category = new Category()
            {
                Name = request.Name,
            };

            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return Created();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCustomerRequest request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category is null)
                return NotFound("category not found");

            category.Name = request.Name;
           
            _context.Update(category);
            await _context.SaveChangesAsync();

            return Ok(category);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
                return NotFound("customer not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok("custmer deleted");
        }
    }
}
