using Library_System.Data;
using Library_System.Entities;
using Library_System.BorrowerDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BorrowerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAllBorrowers()
        {
            var borrowers = await _context.Borrowers.ToListAsync();
            return Ok(borrowers);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBorrowerByBookIdAndBorrowerId(int id)
        {
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(x=>x.Id == id);

            if (borrower is null)
                return NotFound("not found");


            var response = new GetBorrowerResponse
            {
               Id = borrower.Id,
               Name = borrower.Name,
               MembershipDate = borrower.MembershipDate,

            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createBorrower(CreateBorrowerRequest request)
        {
            var borrower = new Borrower()
            {
               Id= request.Id,
               Name = request.Name,
               MembershipDate= request.MembershipDate,
            };

            _context.Borrowers.Add(borrower);
            _context.SaveChanges();

            return Ok(borrower);

        }


        [HttpPut]
        public async Task<IActionResult> updateBorrower(UpdateBorrowerRequest request)
        {
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(x =>x.Id == request.Id);

            if (borrower is null)
                return NotFound("not found");


            borrower.Name = request.Name;

            _context.Borrowers.Update(borrower);
            _context.SaveChanges();

            return Ok(borrower);
        }



        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(x=>x.Id == id);
            if (borrower is null)
                return NotFound("not found");

            _context.Borrowers.Remove(borrower);
            _context.SaveChanges();

            return Ok(borrower);
        }
    }
}
