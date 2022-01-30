using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext dbcontext, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        public Token Handle()
        {
            // Tokenla user bul aynı zamanda tokenın süresi geçmiş mi diye kontrol et
            var user = _dbcontext.Users.FirstOrDefault(x=>x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            // geçmediyse yeni bir tane yap
            if(user is not null)
            {
                // yeni token yap
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);
                // usera ata
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbcontext.SaveChanges();

                return token;
            }else
            {
                throw new InvalidOperationException("Geçerli bir Refresh Token Bulunamadı!");
            }
        }

    }
}