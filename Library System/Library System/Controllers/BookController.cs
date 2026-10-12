using Library_System.BookDTOs;
using Library_System.Data;
using Library_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("id")]
        public async Task<IActionResult> getBookById(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book is null)
                return NotFound("not found");


            var response = new GetBookResponsecs
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createBook(CreateBookRequest request)
        {
            var book = new Book()
            {
                Id = request.Id,
                Title = request.Title,
                AuthorId = request.AuthorId,
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok(book);

        }


        [HttpPut]
        public async Task<IActionResult> updateBook(UpdateBookRequest request)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (book is null)
                return NotFound("not found");


            book.Title = request.Title;
            
            _context.Books.Update(book);
            _context.SaveChanges();

            return Ok(book);
        }



        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var book = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (book is null)
                return NotFound("not found");

            _context.Authors.Remove(book);
            _context.SaveChanges();

            return Ok(book);
        }
    }
}
