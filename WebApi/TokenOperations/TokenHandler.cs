using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            // Usera özel token yapıyor
            Token tokenModel = new Token();
            // security key kullanarak token oluşturuyorsun configurasyon appsettings de
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            // burda keyi şifreliyor >> hazır şifreleme algoritması kullanıyorsun
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Expiration'ı 15 dk ya ayarla
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);
            // Token ayarları
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore : DateTime.Now, // oluşturulduktan şu kadar dk sonra kullanılmaya başlansın
                // şifrelenmiş key buraya
                signingCredentials : credentials
            );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // Token oluşturuluyor
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;
        }
        // Refresh token veren metod
        public string CreateRefreshToken(){
            return Guid.NewGuid().ToString();
        }

    }
}