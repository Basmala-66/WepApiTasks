using E_commerce_System.Data;
using E_commerce_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            return Ok(orders);
        }




        [HttpGet("getOrder")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
                return NotFound("Order not found");

            var response = new GetOrderResponse
            {
                Id = order.Id,
                Date = order.OrderDate,
                CustomerId = order.CustomerId
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var customerExists = await _context.Customers
                .AnyAsync(x => x.Id == request.CustomerId);

            if (!customerExists)
                return BadRequest("Customer not found");

            var order = new Order
            {
                OrderDate = request.Date,
                CustomerId = request.CustomerId
            };


            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = order.Id },
                new GetOrderResponse
                {
                    Id = order.Id,
                    Date = order.OrderDate,
                    CustomerId = order.CustomerId
                });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
                return NotFound("Order not found");

            var customerExists = await _context.Customers
                .AnyAsync(c => c.Id == request.CustomerId);

            if (!customerExists)
                return BadRequest("Customer not found");

            order.OrderDate = request.Date;
            order.CustomerId = request.CustomerId;

            await _context.SaveChangesAsync();

            return Ok(new GetOrderResponse
            {
                Id = order.Id,
                Date = order.OrderDate,
                CustomerId = order.CustomerId
            });
        }

            [HttpDelete]
            public async Task<IActionResult> DeleteOrder(int id)
            {
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order is null)
                    return NotFound("Order not found");

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                return NoContent();
            } 
        
        }
}

