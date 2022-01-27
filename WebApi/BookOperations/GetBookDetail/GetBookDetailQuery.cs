using System;
using AutoMapper;
using WebApi.Common;
using WebApi.Controllers;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookID { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle(){
            Book book = _dbContext.Books.Find(BookID);
            if(book is null){
                throw new InvalidOperationException("Kitap Bulunamadı!");
            }
            // book modeli BookDetail Modele dönüştürülüyor
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
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