using BooksAPIsv2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Models
{
    public class Book
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Sumary { get; set; }
        public string Author { get; set; }
        public int IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public Category category { get; set; }
    
    }
}
