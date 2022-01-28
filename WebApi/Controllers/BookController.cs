using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    // Bunları parçalamamızın nedeni okunabilirliği ve yönetilebilirliği arttırmak

    [ApiController] // api controller olduğunu belirt
    [Route("[controller]s")] // site/Book + s şeklinde ulaşılacak
    public class BookController : ControllerBase
    {
        // dependency injection
        private readonly BookStoreDbContext _context; // dependency
        private readonly IMapper _mapper;
        // readonly değişkenler sadece contructor içinde set edilebilirler
        public BookController(BookStoreDbContext context, IMapper mapper) // dependency injection
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] // sadece bir tane parametresiz get olabilir
        public IActionResult GetBooks()
        {
            // tanımladığımız classdan metodu çağırdık
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            // OK response'u içinde kitapları döndürdük
            return Ok(result);
        }

        [HttpGet("{id}")] // from route
        public IActionResult GetByID(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookID = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
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
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            // modeli doğrula
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            // Eğer model valid değilse hata bas
            // artık try catch yerine custom middleware kullanıyoruz
            validator.ValidateAndThrow(command);
            // Yoksa kaydet
            command.Handle();

            return Ok();
        }

        // Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookID = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookID = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }


    }
}