using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        // test için sahte db üret eklentileri test projesine ekle
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        // constructor ekle
        public CommonTestFixture()
        {
            // kullanıcak sahte database'i inmemory olarak ayarla
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            // olan dbcontexti kullanarak test içinde başka db yaptık
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated(); // DB oluşturulduğundan emin ol
            Context.AddBooks(); // verileri ekle
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges(); // tabloları kaydet

            // Configurasyonları orjinal projeden al
            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
        
    }
}