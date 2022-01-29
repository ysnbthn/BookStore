using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        // testFixture verilerini alıyoruz
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenWrongBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel() { Title = "Hobbit" };
            command.BookID = 5; 
            command.Model = model;
            // act & assert
            FluentActions.Invoking( ()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And
                .Message.Should().Be("Güncelleyecek Kitap Bulunamadı!");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdate()
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel() { Title = "Hobbit" };
            command.BookID = 1; 
            command.Model = model;
            var beforeUpdate = _context.Books.Find(command.BookID);
            beforeUpdate.Title = model.Title;
            // act
            FluentActions.Invoking( ()=> command.Handle()).Invoke();
            // assert
            var afterUpdate = _context.Books.Find(command.BookID);
            afterUpdate.Should().NotBeNull();
            afterUpdate.Should().BeEquivalentTo(beforeUpdate);
        }

    }
}