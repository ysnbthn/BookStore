using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
             context.Genres.AddRange(
                    new Genre { ID = 1, Name = "Personal Growth" },
                    new Genre { ID = 2, Name = "Sience Fiction" },
                    new Genre { ID = 3, Name = "Romance" }
                );
        }
    }
}