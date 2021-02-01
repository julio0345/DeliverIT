using System;

namespace ContasPagar.ViewModels
{
    public class ContaPagarList
    {
        #region Construtor
        public ContaPagarList(string nome, decimal valorOriginal, decimal valorCorrigido, int diasAtraso, DateTime dataPagamento)
        {
            Nome = nome;
            ValorOriginal = valorOriginal;
            ValorCorrigido = valorCorrigido;
            DiasAtraso = diasAtraso;
            DataPagamento = dataPagamento;
        }

        #endregion

        #region Atributos
        public string Nome { get; private set; }

        public decimal ValorOriginal { get; private set; }

        public decimal ValorCorrigido { get; private set; }

        public int DiasAtraso { get; private set; }

        public DateTime DataPagamento { get; private set; }

        #endregion
    }
}