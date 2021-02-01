using ContasPagar.Models;
using ContasPagar.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.Services
{
    public class CalculosService : ICalculosService
    {
        public int CalcularDiasAtraso(ContaPagar cont)
        {
            int dias = Convert.ToInt32(cont.DataPagamento.Subtract(cont.DataVencimento).TotalDays);
            return dias > 0 ? dias : 0;
        }

        public decimal CalcularValorCorrigido(ContaPagar cont, List<Regra> regras)
        {
            decimal valorCorrigido = cont.ValorOriginal;

            if (cont.DiasAtraso > 0)
            {
                foreach (Regra item in regras.OrderByDescending( c => c.DiasMinimoAtraso))
                {
                    if (cont.DiasAtraso >= item.DiasMinimoAtraso)
                    {
                        valorCorrigido = cont.ValorOriginal
                            + (cont.ValorOriginal * item.MultaPercentual / 100)
                            + (cont.ValorOriginal * (item.JurosDiarioPercentual * cont.DiasAtraso / 100));
                        break;
                    }
                }
            }
            return valorCorrigido;
        }
    }
}
