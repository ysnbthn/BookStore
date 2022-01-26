
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Controllers
{
    public class Book
    {
        // Personal Growth GenreID 1, 
        // Science Fiction GenreID 2 
        // auto increment ekle
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public int GenreID { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}