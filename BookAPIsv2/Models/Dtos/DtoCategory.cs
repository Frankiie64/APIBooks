using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Models.Dtos
{
    public class DtoCategory
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Es necesario que ingrese el nombre de la categoria.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Escriba alguna descripcion para esta categoria.")]
        public string Sumary { get; set; }
    }
}
