using E_commerce_System.Data;
using E_commerce_System.Entities;
using E_commerce_System.OrderDetailsDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderDetailsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var details = await _context.OrderDetails.ToListAsync();



            return Ok(details);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> GetOrderDetailById(int orderId, int productId)
        {
            var detail = await _context.OrderDetails
                    .FirstOrDefaultAsync(d =>
                    d.OrderId == orderId &&
                    d.ProductId == productId);

            if (detail is null)
                return NotFound("Order detail not found");

            var response = new GetOrderDetailResponse
            {
                OrderId = detail.OrderId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(
            CreateOrderDetailRequest request)
        {
            var orderExists = await _context.Orders
                .AnyAsync(o => o.Id == request.OrderId);

            if (!orderExists)
                return BadRequest("Order not found");

            var productExists = await _context.Products
                .AnyAsync(p => p.Id == request.ProductId);

            if (!productExists)
                return BadRequest("Product not found");

            var detail = new OrderDetail
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _context.OrderDetails.AddAsync(detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOrderDetailById),
                new
                {
                    orderId = detail.OrderId,
                    productId = detail.ProductId
                },
                new GetOrderDetailResponse
                {
                    OrderId = detail.OrderId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity
                });
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> UpdateOrderDetail(
            int orderId,
            int productId,
            UpdateOrderDetailRequest request)
        {
            var detail = await _context.OrderDetails
                .FirstOrDefaultAsync(d =>
                    d.OrderId == orderId &&
                    d.ProductId == productId);

            if (detail is null)
                return NotFound("Order detail not found");

            detail.Quantity = request.Quantity;

            await _context.SaveChangesAsync();

            return Ok(new GetOrderDetailResponse
            {
                OrderId = detail.OrderId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity
            });
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderDetail(
            int orderId, int productId)
        {
            var detail = await _context.OrderDetails
                .FirstOrDefaultAsync(d =>
                    d.OrderId == orderId &&
                    d.ProductId == productId);

            if (detail is null)
                return NotFound("Order detail not found");

            _context.OrderDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
