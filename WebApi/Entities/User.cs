using System;

namespace WebApi.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; } 

        // access token >> authenticate olmak için kullandığın token biterse tekrar alman gerekir
        // refresh token >> kullanıcı sitedeyken access token süresi dolunca 
        // tekrar logine atmak yerine otomatik yeni access token alıp refresh token'ı yeniliyorsun
        // refresh token ve access token süresi dolarsa tekrar login olması gerekiyor
        // daha gelişmiş kütüphaneler var bu en basit hali, bunun için ayrı bir server oluyor
        // giriş ve yetkilendirme ayrı oluyor normalde
    }
}