using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        // genre oluşturulunca otomatik true geliyor
        public bool IsActive { get; set; } = true;
    }
}