using Library_System.BookDTOs;
using Library_System.Data;
using Library_System.Entities;
using Library_System.LoanDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {

        private readonly AppDbContext _context;

        public LoanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAllLoans()
        {
            var loans = await _context.Loans.ToListAsync();
            return Ok(loans);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetLoanByBookIdAndBorrowerId(int bookId,int borrowerId)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.BookId == bookId && l.BorrowerId == borrowerId);

            if (loan is null)
                return NotFound("not found");


            var response = new GetLoanResponse
            {
                BookId = loan.BookId,
                BorrowsId = loan.BorrowerId,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate,

            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createLoan(CreateLoanRequest request)
        {
            var loan = new Loan()
            {
               BookId = request.BookId,
               BorrowerId = request.BorrowsId,
               LoanDate = request.LoanDate,
            };

            _context.Loans.Add(loan);
            _context.SaveChanges();

            return Ok(loan);

        }


        [HttpPut]
        public async Task<IActionResult> updateLoan(UpdateLoanRequest request)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(x =>x.BookId == request.BookId && x.BorrowerId == request.BorrowsId);

            if (loan is null)
                return NotFound("not found");


            loan.ReturnDate = request.ReturnDate;

            _context.Loans.Update(loan);
            _context.SaveChanges();

            return Ok(loan);
        }



        [HttpDelete]
        public async Task<IActionResult> delete(int bookId, int borrowerId)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.BookId == bookId && l.BorrowerId == borrowerId);
            if (loan is null)
                return NotFound("not found");

            _context.Loans.Remove(loan);
            _context.SaveChanges();

            return Ok(loan);
        }
    }
}
