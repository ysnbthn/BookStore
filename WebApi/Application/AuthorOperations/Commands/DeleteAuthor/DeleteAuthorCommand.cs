using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int ID { get; set; }

        private readonly IBookStoreDbContext _dbContext;

        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            Author author = _dbContext.Authors.Find(ID);
            
            if(author is null)
            {
                throw new InvalidOperationException("Silinecek Yazar bulunamadı");
            }
            
            if(_dbContext.Books.Any(a=>a.AuthorID == ID))
            {
                throw new InvalidOperationException("Yazarı Silmeden Önce Yazarın Kitaplarını Siliniz!");
            }
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}