using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        // Fact bir koşulda bir kes çalışır
        [Theory] // birden fazla veri varsa ve her veri için testin çalışmasını istiyorsan bunu kullan
        [InlineData("Lord Of The Rings",0,0,0)] // kullanıcıdan gelen veriler
        [InlineData("Lord Of The Rings",0,0,1)] // her koşul için bir tane yaz sınır yok
        [InlineData("",0,0,0)]
        [InlineData("",100,1,1)] 
        [InlineData("",0,1,1)]   
        [InlineData("Lor",100,1,1)] 
        [InlineData("Lord",1,1,0)] 
        //[InlineData("Lord Of The Rings",1,2,1)] // hatalı test
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreID, int authorID)
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {   
                Title = title, 
                PageCount = pageCount, 
                PublishDate = DateTime.Now.AddYears(-15), //DateTimeNow sürekli değişiyor ayrı case lazım 
                GenreID = genreID, 
                AuthorID = authorID
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {   
                Title = "Lord Of The Rings", 
                PageCount = 1000, 
                PublishDate = DateTime.Now.Date, // hata versin diye
                GenreID = 2, 
                AuthorID = 1
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        // Herşeyin olması gerektiği gibi çalışması durumu
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {   
                Title = "Lord Of The Rings", 
                PageCount = 1000, 
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreID = 2, 
                AuthorID = 1
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}