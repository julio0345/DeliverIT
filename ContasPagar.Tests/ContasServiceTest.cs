using ContasPagar.Models;
using ContasPagar.Services;
using ContasPagar.Services.Interfaces;
using Moq;
using System;
using Xunit;

namespace ContasPagar.Tests
{
    public class ContasServiceTest
    {
        private readonly ContasService _contasServiceMock;

        public ContasServiceTest()
        {
            _contasServiceMock = new ContasService(null);
        }


        [Fact(DisplayName = "Quando Houver Uma Conta a Pagar Calcular os Dias em Atraso")]
        public void QuandoHouverUmaContaAPagarCalcularDiasAtraso()
        {
            var conta = new ContaPagar()
            {
                Id = Guid.NewGuid(),
                Nome = "SetupTest",
                ValorOriginal = 0m,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now.AddDays(5)
            };

            var result = _contasServiceMock.CalcularDiasAtraso(conta);

            Assert.Equal(5, result);
        }
    }
}
