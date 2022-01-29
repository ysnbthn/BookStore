using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IMapper mapper, IBookStoreDbContext dbcontext, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbcontext = dbcontext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbcontext.Users.FirstOrDefault(x=>x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null){
                // Tokenı üret
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                // access token'ın 15 dk süresi var refreshe 5 dk daha ekle ki 
                // gelip yeni access token alabilsin
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbcontext.SaveChanges();
                return token;
            }else{
                throw new InvalidOperationException("Kullanıcı Adı veya Şifre Hatalı!");
            }
        }

    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}