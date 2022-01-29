using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public int BookID { get; set; }
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {

            Book book = _dbContext.Books.SingleOrDefault(x => x.ID == BookID);
            if (book is null)
            {
                throw new InvalidOperationException("Güncelleyecek Kitap Bulunamadı!");
            }
            // tek tek kontrol et
            book.GenreID = Model.GenreID != default ? Model.GenreID : book.GenreID;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != "string" ? Model.Title : book.Title;

            _dbContext.SaveChanges();
        }

    }

    // Her operasyon için ayrı model yapıyoruz
    // çünkü bazı operasyonlar birbirinden farklı veriler isteyebilirler
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreID { get; set; }

    }
}



