using System;
using WebApi.Common;
using WebApi.Controllers;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookID { get; set; }
        public GetBookDetailsQuery(BookStoreDbContext dbContext){
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle(){
            Book book = _dbContext.Books.Find(BookID);
            if(book is null){
                throw new InvalidOperationException("Kitap Bulunamadı!");
            }
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre = ((GenreEnum)book.GenreID).ToString();
            return vm;
        }

    }

    public class BookDetailViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}