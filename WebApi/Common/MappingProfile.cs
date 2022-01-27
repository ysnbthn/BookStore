using AutoMapper;
using WebApi.BookOperations.CreateBookCommand;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.Controllers;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        // profilleri burdan al
        public MappingProfile()
        {
            // createbook model objesi book objesine maplenebilir/dönüştürülebilir
            CreateMap<CreateBookModel, Book>();
            // burda bir şey değişik ondan elle ayarlıyoruz
            CreateMap<Book, BookDetailViewModel>().ForMember(
                                        dest => dest.Genre, 
                                        opt=> opt.MapFrom(src=> ((GenreEnum)src.GenreID).ToString())
                                        );
            // Bookta GenreID nin GenreEnumdaki karşılığını string yap BooksViewModeldeki Genreya ata
            CreateMap<Book, BooksViewModel>().ForMember(
                                        dest => dest.Genre, 
                                        opt=> opt.MapFrom(src=> ((GenreEnum)src.GenreID).ToString())
                                        );;
        }
    }
}