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
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var categories = await _context.Customers.ToListAsync();

            return Ok(categories);
        }



        [HttpGet("getCustomer")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var custmer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (custmer is null)
                return NotFound($"customer with i ({id}) is not found");

            var response = new GetCustomerResponse()
            {
                Id = custmer.Id,
                Name = custmer.Name
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustmoer(CreateCustomerRequest request)
        {
            var customer = new Customer()
            {
                Name = request.Name,
                Email = request.Email
            };

            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();

            return Created();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequest request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (customer is null)
                return NotFound("customer not found");

            customer.Name = request.Name;
            customer.Email = request.Email;

            _context.Update(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer is null)
                return NotFound("customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("customer deleted");
        }
    }
}
