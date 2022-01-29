using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
    // mapperla contexte erişmesi için
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        // testFixture verilerini alıyoruz
        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact] // sonucunda birşey kontrol edilmesi gereken test olduğunu belirtiyorsun
         public void WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange >> Hazırlık
            //  Bunu kullanıcıdan gelen veri olarak düşün
            var book = new Book(){ Title = "Test_WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn", PageCount=100, PublishDate= new System.DateTime(1990,01,10), GenreID=1, AuthorID=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){ Title = book.Title };

            // act >>>>>> Çalıştırma (Doğrulama ile aynı anda yapılabilir)
            FluentActions
                .Invoking( ()=> command.Handle()) // act kısmı
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut"); // assert kısmı
            // assert >>> Doğrulama
        }

       [Fact] // Happy Path
       public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
       {
           // arrange
           CreateBookCommand command = new CreateBookCommand(_context, _mapper);
           CreateBookModel model = new CreateBookModel(){ Title="Hobbit", PageCount=1000, PublishDate= DateTime.Now.Date.AddYears(-10), GenreID=1, AuthorID=1 };
           command.Model = model;
            // act
            FluentActions.Invoking( ()=> command.Handle()).Invoke(); // direk çalıştırıyorsun
            // assert
            // Sonra veri tabanını kontrol ediyorsun
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.Should().BeEquivalentTo(book); // toptan kontrol ediyorsun
            //book.PageCount.Should().Be(model.PageCount); // istersen böyle tek tek kontrol et
       } 
        
    }
}