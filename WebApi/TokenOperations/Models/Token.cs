using System;

namespace WebApi.TokenOperations.Models
{
    public class Token
    {
        // geriye dönülecek verileri tutan obje
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}