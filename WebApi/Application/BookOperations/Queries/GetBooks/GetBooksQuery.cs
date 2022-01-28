using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{
    // get books API içindekileri aldık 
    // komple ayrı bir sınıf ve metod class olarak tanımladık
    public class GetBooksQuery
    {
        // diğerlerindeki gibi context aldık
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // tüm bookları alma metodu
        public List<BooksViewModel> Handle(){
            List<Book> booklist = _dbContext.Books.Include(x => x.Genre).OrderBy(x => x.ID).ToList();
            // booklist BooksViewModel listesine dönüştürülüyor
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(booklist);
            
            // Verileri çekip View Modele atadık
            //List<BooksViewModel> vm = new List<BooksViewModel>();
            // foreach(var book in booklist){
            //     vm.Add(new BooksViewModel(){
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreID).ToString(),
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            //         PageCount = book.PageCount
            //     });
            // }
            return vm;
        }


    }
    // veri güvenliği için View Model yaptık
    public class BooksViewModel {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}