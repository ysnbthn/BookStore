using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    public class BookStoreDbContext : DbContext
    {
        // add constructor for dbcontext
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options){}

        // dbContextdeki isim her zaman çoğul olur
        public DbSet<Book> Books { get; set; }
        

    }
}