using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoCRUD.Models
{
    public class Livro
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public int AnoEdicao { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public Genero Genero { get; set; }
        public int GeneroId { get; set; }
    }
}