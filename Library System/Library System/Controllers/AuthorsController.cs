using Library_System.AuthorDTOs;
using Library_System.Data;
using Library_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
         _context = context;   
        }

        [HttpGet]
        public async Task<IActionResult> getAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }

        [HttpGet("id")]
        public async Task<IActionResult> getAuthorById(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author is null)
                return NotFound("not found");


            var response = new GetAuthorResponse
            {
                Id = author.Id,
                Name= author.Name
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createAuthor(CreateAuthorRequest request)
        {
            var author = new Author()
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            return Ok(author);

        }


        [HttpPut]
        public async Task<IActionResult> updateAuthor(UpdateAuthorRequest request)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(author is  null) 
                return NotFound("not found");


            author.Name = request.Name;
            author.BirthDate = request.BirthDate;

            _context.Authors.Update(author);
            _context.SaveChanges();

            return Ok(author);
        }


        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author is null)
                return NotFound("not found");

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return Ok(author);
        }
    }
}
