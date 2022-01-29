using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    // create books API içindekileri aldık 
    // komple ayrı bir sınıf ve metod class olarak tanımladık
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle(){
            Book book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if(book is not null){
                // API Controllerda olmadığımız için bunu yapabiliyoruz
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            
            if(!_dbContext.Authors.Any(x=>x.AuthorID == Model.AuthorID)){
                throw new InvalidOperationException("Böyle Bir Yazar Yok!");
            }

            // Model ile gelen veriyi book objesine çevir
            book = _mapper.Map<Book>(Model); // new Book();

            // her değişiklikten sonra _contexti kaydet
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }


    }

    // view Model sadece UI a dönmek için kullanılıyor
    public class CreateBookModel 
    {    
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreID { get; set; }
        public int AuthorID { get; set; }

    }
}