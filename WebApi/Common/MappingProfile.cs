using AutoMapper;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

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
            CreateMap<Book, BookDetailViewModel>()
                                        .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                                        .ForMember(dest2 => dest2.Author, opt2 => opt2.MapFrom(src => src.Author.AuthorName + " " + src.Author.AuthorSurname));
            // Tablo olduğu için Enuma gerek kalmadı artık direk tabloyu çekerken include ediyoruz
            // ordan direk genre.name ile ulaşabiliyoruz

            CreateMap<Book, BooksViewModel>()
                                        .ForMember(dest2 => dest2.Author, opt2 => opt2.MapFrom(src => src.Author.AuthorName + " " + src.Author.AuthorSurname))
                                        .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            // Genre için aynılarını yap                                
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            //  Author için aynılarını yap, ismi tek seferde alcaksan formember de maple
            CreateMap<Author,AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();

        }
    }
}