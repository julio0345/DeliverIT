using ContasPagar.Models;
using ContasPagar.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContasPagar.Services.Interfaces
{
    public interface IContasService
    {
        Task<IEnumerable<ContaPagarList>> RetornarListaContas();

        Task<ContaPagar> AplicarRegras(ContaPagarPost contaPost);
    }
}