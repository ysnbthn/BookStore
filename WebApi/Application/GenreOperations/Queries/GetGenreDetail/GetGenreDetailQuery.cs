using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public GenreDetailViewModel Handle()
        {
            Genre genre = _context.Genres.SingleOrDefault(x=>x.ID == GenreID && x.IsActive);
            
            if(genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı yada Aktif Değil!");
            }

            return _mapper.Map<GenreDetailViewModel>(genre);
        }

    }

    public class GenreDetailViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}