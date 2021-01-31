using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.Models
{
    public class Regra
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int DiasMinimoAtraso { get; set; }

        [Required]
        [Column(TypeName = "numeric(6,3)")]
        public decimal MultaPercentual { get; set; }

        [Required]
        [Column(TypeName = "numeric(6,3)")]
        public decimal JurosDiarioPercentual { get; set; }
    }
}