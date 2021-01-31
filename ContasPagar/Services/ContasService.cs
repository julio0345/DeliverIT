using ContasPagar.InfraStructure;
using ContasPagar.Models;
using ContasPagar.Services.Interfaces;
using ContasPagar.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.Services
{
    public class ContasService : IContasService
    {
        private readonly ContasContext _context;

        public ContasService(ContasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContaPagarList>> RetornarListaContas()
        {
            var contas = await _context.ContaPagar.ToListAsync();
            var contasConvertidas = contas.Select(cont => new ContaPagarList(cont.Nome, cont.ValorOriginal,
                cont.ValorCorrigido, cont.DiasAtraso, cont.DataPagamento));

            return contasConvertidas;
        }

        public async Task<ContaPagar> AplicarRegras(ContaPagarPost contaPost)
        {
            var conta = contaPost.ToModel();
            var regras = await _context.Regras.OrderByDescending(c => c.DiasMinimoAtraso).ToListAsync();

            conta.DiasAtraso = CalcularDiasAtraso(conta);
            conta.ValorCorrigido = CalcularValorCorrigido(conta, regras);

            return conta;
        }

        private int CalcularDiasAtraso(ContaPagar cont)
        {
            int dias = Convert.ToInt32(cont.DataPagamento.Subtract(cont.DataVencimento).TotalDays);
            return dias > 0 ? dias : 0;
        }

        private decimal CalcularValorCorrigido(ContaPagar cont, List<Regra> regras)
        {
            int diasAtraso = CalcularDiasAtraso(cont);

            decimal valorCorrigido = cont.ValorOriginal;

            if (diasAtraso > 0)
            {
                foreach (Regra item in regras)
                {
                    if (diasAtraso >= item.DiasMinimoAtraso)
                    {
                        valorCorrigido = cont.ValorOriginal
                            + (cont.ValorOriginal * item.MultaPercentual / 100)
                            + (cont.ValorOriginal * (item.JurosDiarioPercentual * diasAtraso / 100));
                        break;
                    }
                }
            }
            
            return valorCorrigido;
        }
    }
}
