using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBookCommand;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Controllers
{
    // Bunları parçalamamızın nedeni okunabilirliği ve yönetilebilirliği arttırmak

    [ApiController] // api controller olduğunu belirt
    [Route("[controller]s")] // site/Book + s şeklinde ulaşılacak
    public class BookController : ControllerBase
    {
        // dependency injection
        private readonly BookStoreDbContext _context;
        // readonly değişkenler sadece contructor içinde set edilebilirler
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet] // sadece bir tane parametresiz get olabilir
        public IActionResult GetBooks()
        {
            // tanımladığımız classdan metodu çağırdık
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            // OK response'u içinde kitapları döndürdük
            return Ok(result);
        }

        [HttpGet("{id}")] // from route
        public IActionResult GetByID(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailsQuery query = new GetBookDetailsQuery(_context);
                query.BookID = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // [HttpGet] // from query
        // query yerine route tercih etmeye çalış
        // public Book Get([FromQuery] string id)
        // {

        //     Book book = BookList.Where(x => x.ID == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        // Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context);
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                // eğer sorun olursa badrequest ver 
                // createBookCommandaki exception mesajını bas
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookID = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookID = id;
                command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


    }
}