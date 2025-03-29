using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuccasCorpVX.Models
{
    public class Professores
    {
        [Key]
        [StringLength(100)]
        public string Id { get; set; } // Chave primária

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Campus")]
        public string Campus { get; set; }

        [StringLength(100)]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now; // Data e hora de envio

        public bool Ativo { get; set; }

        public double Media { get; set; }

        public string AvaliacaoGeral { get; set; }

        public int TotalComentarios { get; set; }

    }
}