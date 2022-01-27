using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.Controllers;

namespace WebApi.BookOperations.CreateBookCommand
{
    // create books API içindekileri aldık 
    // komple ayrı bir sınıf ve metod class olarak tanımladık
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle(){
            Book book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if(book is not null){
                // aPI Controllerda olmadığımız için bunu yapabiliyoruz
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            // Model ile gelen veriyi book objesine çevir
            book = _mapper.Map<Book>(Model); // new Book();
            // book.Title = Model.Title;
            // book.PageCount = Model.PageCount;
            // book.PublishDate = Model.PublishDate;
            // book.GenreID = Model.GenreID;

            // her değişiklikten sonra _contexti kaydet
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }


    }

    // view Model sadece UI a dönmek için kullanılıyor
    public class CreateBookModel {    
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreID { get; set; }

    }
}