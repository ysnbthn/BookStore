using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int ID { get; set; }

        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            Author author = _dbContext.Authors.Find(ID);
            if (author is null)
            {
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!");
            }
            author.AuthorName = Model.Name != "string" ? Model.Name : author.AuthorName;
            author.AuthorSurname = Model.Surname != "string" ? Model.Surname : author.AuthorSurname;
            author.BirthDate = Model.BirthDate != default ? Model.BirthDate.Date : author.BirthDate;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}