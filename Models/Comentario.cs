using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuccasCorpVX.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; } // Chave primária

        [Required]
        [StringLength(100)]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Descricao")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Required]
        public string Autor { get; set; }

        public int Professor { get; set; }
        public int Disciplina { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now; // Data e hora de envio
        public string Sentimento { get; set; }
        public double Positivo { get; set; }
        public double Negativo { get; set; }
        public double Neutro { get; set; }
        public bool Insulto { get; set; }
        public bool Ativo { get; set; }
    }
}