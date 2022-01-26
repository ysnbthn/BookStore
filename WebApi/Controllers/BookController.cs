using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [ApiController] // api controller olduğunu belirt
    [Route("[controller]s")] // site/Book + s şeklinde ulaşılacak
    public class BookController : ControllerBase
    {
        // Dummy Data
        private static List<Book> BookList = new List<Book>(){
            
        };

        [HttpGet] // sadece bir tane parametresiz get olabilir
        public List<Book> GetBooks(){
            List<Book> booklist = BookList.OrderBy(x => x.ID).ToList();
            return booklist;
        }

        [HttpGet("{id}")] // from route
        public Book GetByID(int id)
        {
            Book book = BookList.Where(x => x.ID == id).SingleOrDefault();
            return book;
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
        public IActionResult AddBook([FromBody] Book newBook){
            // önce id var mı kontrol et sonra id boş gelceği için isme çevir
            Book book = BookList.SingleOrDefault(x => x.ID == newBook.ID);
            if(book is not null){
                return BadRequest();
            }
            BookList.Add(newBook);
            return Ok();
        }

        // Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
            var book = BookList.SingleOrDefault(x=>x.ID == id);
            if(book is null){
                return BadRequest();
            }
            // Şuan EF Core olmadığı için elle tek tek kontrol et
            book.GenreID = updatedBook.GenreID != default ? updatedBook.GenreID : book.GenreID;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            Book book = BookList.SingleOrDefault(x => x.ID == id);
            if(book == null){
                return BadRequest();
            }
            BookList.Remove(book);
            return Ok();
        }


    }
}