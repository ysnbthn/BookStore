using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorID { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public AuthorDetailViewModel Handle()
        {
            Author author = _context.Authors.SingleOrDefault(x => x.AuthorID == AuthorID);
            if(author is null){
                throw new InvalidOperationException("Yazar Bulunamadı!");
            }
            return _mapper.Map<AuthorDetailViewModel>(author);
        }
    }

    public class AuthorDetailViewModel
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string BirthDate { get; set; }
    }
}