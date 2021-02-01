using ContasPagar.Models;
using ContasPagar.Services;
using ContasPagar.Services.Interfaces;
using ContasPagar.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ContasPagar.Tests
{
    public class ContasServiceTest
    {
        ICalculosService _calculosService;

        List<Regra> listaRegras = new List<Regra>();

        public ContasServiceTest()
        {
            _calculosService = new CalculosService();


            listaRegras.Add(new RegraPost(1, 2, 0.1M).ToModel());
            listaRegras.Add(new RegraPost(4, 3, 0.2M).ToModel());
            listaRegras.Add(new RegraPost(6, 5, 0.3M).ToModel());
        }

        [Fact(DisplayName = "Verificação do cálculo de dias de atraso")]
        public void VerificarCalculoDiasAtraso()
        {
            //Arrange
            Random random = new Random();

            int numeroDiasVencimento = random.Next(1,50);
            int numeroDiasPagamento = random.Next(1, 50);

            var conta = new ContaPagar()
            {
                Id = Guid.NewGuid(),
                Nome = "SetupTest",
                ValorOriginal = 0m,
                DataVencimento = DateTime.Now.AddDays(numeroDiasVencimento * -1),
                DataPagamento = DateTime.Now.AddDays(numeroDiasPagamento)
            };

            //Act
            var resultado = _calculosService.CalcularDiasAtraso(conta);

            //Assert
            Assert.Equal(numeroDiasVencimento + numeroDiasPagamento, resultado);
        }

        [Fact(DisplayName = "Verificação do Cálculo do Valor Corrigido com 3 dias de Atraso")]
        public void VerificarCalculoValorCorrigido3DiasAtraso()
        {
            //Arrange
            var conta1 = new ContaPagar()
            {
                Id = Guid.NewGuid(),
                Nome = "Conta de R$ 500,00 com 3 dias de Atraso",
                DiasAtraso = 3,
                ValorOriginal = 500M,
                DataVencimento = DateTime.Now.AddDays(-3),
                DataPagamento = DateTime.Now
            };

            //Act
            var resultadoConta1 = _calculosService.CalcularValorCorrigido(conta1, listaRegras);

            //Assert
            Assert.Equal(resultadoConta1, 511.5M);
        }

        [Fact(DisplayName = "Verificação do Cálculo do Valor Corrigido com 5 dias de Atraso")]
        public void VerificarCalculoValorCorrigido5DiasAtraso()
        {
            //Arrange
            var conta2 = new ContaPagar()
            {
                Id = Guid.NewGuid(),
                Nome = "Conta de R$ 100,00 com 5 dias de Atraso",
                DiasAtraso = 5,
                ValorOriginal = 100M,
                DataVencimento = DateTime.Now.AddDays(-5),
                DataPagamento = DateTime.Now
            };

            //Act
            var resultadoConta2 = _calculosService.CalcularValorCorrigido(conta2, listaRegras);

            //Assert
            Assert.Equal(resultadoConta2, 104M);
        }

        [Fact(DisplayName = "Verificação do Cálculo do Valor Corrigido com 10 dias de Atraso")]
        public void VerificarCalculoValorCorrigido10DiasAtraso()
        {
            //Arrange
            var conta3 = new ContaPagar()
            {
                Id = Guid.NewGuid(),
                Nome = "Conta de R$ 1.000,00 com 10 dias de Atraso",
                DiasAtraso = 10,
                ValorOriginal = 1000M,
                DataVencimento = DateTime.Now.AddDays(-10),
                DataPagamento = DateTime.Now
            };

            //Act
            var resultadoConta3 = _calculosService.CalcularValorCorrigido(conta3, listaRegras);

            //Assert
            Assert.Equal(resultadoConta3, 1080M);
        }

        [Fact(DisplayName = "Verificação do Cálculo do Valor Corrigido pago adiantado")]
        public void VerificarCalculoValorCorrigidoPagoAdiantado()
        {
            //Arrange
            var conta4 = new ContaPagar()
            {
                Id = Guid.NewGuid(),
                Nome = "Conta de R$ 10,50 paga Adiandata",
                DiasAtraso = 0,
                ValorOriginal = 10.5M,
                DataVencimento = DateTime.Now.AddDays(+10),
                DataPagamento = DateTime.Now
            };

            //Act
            var resultadoConta4 = _calculosService.CalcularValorCorrigido(conta4, listaRegras);

            //Assert
            Assert.Equal(resultadoConta4, 10.5M);
        }
    }
}
