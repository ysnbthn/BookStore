using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            Author author = _context.Authors.SingleOrDefault(x=>x.AuthorName == Model.Name && x.AuthorSurname == Model.Surname);
            if(author is not null)
            {
                throw new InvalidOperationException("Yazar Zaten Kayıtlı!");
            }
            author = new Author();
            author.AuthorName = Model.Name;
            author.AuthorSurname = Model.Surname;
            author.BirthDate = Model.BirthDate.Date;
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}