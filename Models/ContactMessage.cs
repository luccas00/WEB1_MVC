using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuccasCorpVX.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; } // Chave primária

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mensagem")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public DateTime DateSent { get; set; } = DateTime.Now; // Data e hora de envio

        public string Sentimento { get; set; }
        public double Positivo { get; set; }
        public double Negativo { get; set; }
        public double Neutro { get; set; }
        public bool Insulto { get; set; }
        public bool Ativo { get; set; }

    }

}