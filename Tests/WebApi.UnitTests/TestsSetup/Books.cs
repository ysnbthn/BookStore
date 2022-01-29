using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            // sahte db tablelarına data ekle 
             context.Books.AddRange(
                    new Book { Title = "Lean Startup", GenreID = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12), AuthorID = 1 },
                    new Book { Title = "Herland", GenreID = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23), AuthorID = 2 },
                    new Book { Title = "Dune", GenreID = 2, PageCount = 540, PublishDate = new DateTime(2001, 12, 21) , AuthorID = 3}
                );
        }
    }
}