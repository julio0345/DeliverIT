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

        private readonly ICalculosService _calculosService;

        public ContasService(ContasContext context, ICalculosService calculosService)
        {
            _context = context;
            _calculosService = calculosService;
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
            var regras = await _context.Regras.ToListAsync();

            conta.DiasAtraso = _calculosService.CalcularDiasAtraso(conta);
            conta.ValorCorrigido = _calculosService.CalcularValorCorrigido(conta, regras);

            return conta;
        }
    }
}