using ContasPagar.Models;
using System;

namespace ContasPagar.ViewModels
{
    public class ContaPagarPost
    {
        public string Nome { get; set; }

        public decimal ValorOriginal { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime DataPagamento { get; set; }

        public ContaPagar ToModel()
        {
            return new ContaPagar
            {
                Id = Guid.NewGuid(),
                Nome = this.Nome,
                ValorOriginal = this.ValorOriginal,
                DataPagamento = this.DataPagamento,
                DataVencimento = this.DataVencimento,
            };
        }
    }
}