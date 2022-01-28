using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        // in memorye database verileri insert etmek için kullanılıyor
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // dependency injection
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // veri tabanına eriş veri varsa return 
                if (context.Books.Any())
                {
                    return;
                }
                // yoksa ekle
                context.Books.AddRange(
                    new Book { Title = "Lean Startup", GenreID = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12), AuthorID = 1 },
                    new Book { Title = "Herland", GenreID = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23), AuthorID = 2 },
                    new Book { Title = "Dune", GenreID = 2, PageCount = 540, PublishDate = new DateTime(2001, 12, 21) , AuthorID = 3}
                );

                context.Genres.AddRange(
                    new Genre { ID = 1, Name = "Personal Growth" },
                    new Genre { ID = 2, Name = "Sience Fiction" },
                    new Genre { ID = 3, Name = "Romance" }
                );

                context.Authors.AddRange(
                    new Author { AuthorID = 1, AuthorName = "Frank", AuthorSurname="Herbert", BirthDate = new DateTime(1920, 10, 8)},
                    new Author { AuthorID = 2, AuthorName = "Charlotte Perkins", AuthorSurname="Gilman", BirthDate = new DateTime(1860, 7, 3)},
                    new Author { AuthorID = 3, AuthorName = "Eric", AuthorSurname="Ries", BirthDate = new DateTime(1990, 9, 22)}
                );

                // eklediklerimi database'e kaydet
                context.SaveChanges();
            }
        }
    }
}