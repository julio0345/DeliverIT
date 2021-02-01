using ContasPagar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.Services.Interfaces
{
    public interface ICalculosService
    {
        int CalcularDiasAtraso(ContaPagar cont);

        decimal CalcularValorCorrigido(ContaPagar cont, List<Regra> regras);
    }
}