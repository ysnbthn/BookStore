using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        public BookStoreDbContext()
        {
        }

        // add constructor for dbcontext
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        // dbContextdeki isim her zaman çoğul olur
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}