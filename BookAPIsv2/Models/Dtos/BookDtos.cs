using BooksAPIsv2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Models.Dtos
{
    public class BookDtos
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Es necesario que se le asigne un titulo al libro.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Es necesario que se le hago una breve sipnosis al libro.")]

        public string Sumary { get; set; }

        public string Author { get; set; }
        [Required(ErrorMessage = "Es necesario que se le asigne una categoria al libro.")]

        public int IdCategory { get; set; }

        public Category category { get; set; }
    }
}
