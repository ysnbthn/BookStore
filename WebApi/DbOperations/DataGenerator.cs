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
                    new Book { Title = "Lean Startup", GenreID = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
                    new Book { Title = "Herland", GenreID = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23) },
                    new Book { Title = "Dune", GenreID = 2, PageCount = 540, PublishDate = new DateTime(2001, 12, 21) }
                );

                context.Genres.AddRange(
                    new Genre { ID = 1, Name = "Personal Growth" },
                    new Genre { ID = 2, Name = "Sience Fiction" },
                    new Genre { ID = 3, Name = "Romance" }
                );

                // eklediklerimi database'e kaydet
                context.SaveChanges();
            }
        }
    }
}