using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.Models
{
    public class ContaPagar
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "numeric(18,2)")]
        public decimal ValorOriginal { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataVencimento { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataPagamento { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int DiasAtraso { get; set; }

        [Required]
        [Column(TypeName = "numeric(18,2)")]
        public decimal ValorCorrigido { get; set; }
    }
}
