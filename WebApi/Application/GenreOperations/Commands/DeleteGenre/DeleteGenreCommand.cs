using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly IBookStoreDbContext _context;
        public int GenreID { get; set; }
        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            Genre genre = _context.Genres.SingleOrDefault(x => x.ID == GenreID);
            if(genre is null){
                throw new InvalidOperationException("Kitap Türü Bulunamadı!");
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        
        
    }
}