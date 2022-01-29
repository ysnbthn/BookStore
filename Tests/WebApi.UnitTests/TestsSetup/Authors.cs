using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author { AuthorID = 1, AuthorName = "Frank", AuthorSurname="Herbert", BirthDate = new DateTime(1920, 10, 8)},
                    new Author { AuthorID = 2, AuthorName = "Charlotte Perkins", AuthorSurname="Gilman", BirthDate = new DateTime(1860, 7, 3)},
                    new Author { AuthorID = 3, AuthorName = "Eric", AuthorSurname="Ries", BirthDate = new DateTime(1990, 9, 22)}
                );
        }
    }
}