using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuccasCorpVX.Models
{
    public class Disciplinas
    {
        [Key]
        public int Codigo { get; set; } // Chave primária

        public string CodigoUFOP { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Campus")]
        public string Campus { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        public bool Ativo { get; set; }

        public double Media { get; set; }
        
        public string AvaliacaoGeral { get; set; }

    }
}