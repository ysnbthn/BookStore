using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook 
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookID { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext){
            _dbContext = dbContext;
        }

        public void Handle(){

            var book = _dbContext.Books.SingleOrDefault(x => x.ID == BookID);
            
            if(book == null){
                 throw new InvalidOperationException("Silenecek Kitap Bulunamadı!");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

    }
}