using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreID { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            Genre genre = _context.Genres.SingleOrDefault(x => x.ID == GenreID);
            if (genre is null)
            {
                throw new InvalidOperationException("Güncellenecek Kitap Türü Bulunamadı!");
            }

            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.ID != GenreID))
            {
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }

            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();

        }

    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}