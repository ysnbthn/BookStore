using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
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
            Book book = _dbContext.Books.Include(x=>x.Genre).Where(book => book.ID == BookID).SingleOrDefault();
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