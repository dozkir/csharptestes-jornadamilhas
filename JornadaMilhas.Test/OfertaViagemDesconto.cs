using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
        {
            // Arrange
            Rota rota = new Rota("Rio de Janeiro", "Vitória");
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(12));
            double precoOriginal = 100.00;
            double desconto = 20.00;
            double precoComDesconto = precoOriginal - desconto;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            // Act
            oferta.Desconto = desconto;

            // Assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Theory]
        [InlineData(120,30)]
        [InlineData(100,30)]
        [InlineData(0,100)]
        public void RetornaDescontoMaximoQuandoDescontoMaiorOuIgualQuePreco(double desconto, double precoComDesconto)
        {
            // Arrange
            Rota rota = new Rota("Rio de Janeiro", "Vitória");
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(12));
            double precoOriginal = 100.00;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            // Act
            oferta.Desconto = desconto;

            // Assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Fact]
        public void RetornaPrecoSemDescontoQuandoDescontoNegativo()
        {
            // Arrange
            Rota rota = new Rota("Rio de Janeiro", "Vitória");
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(12));
            double precoOriginal = 100.00;
            double desconto = -120.00;
            double precoSemDesconto = precoOriginal;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            // Act
            oferta.Desconto = desconto;

            // Assert
            Assert.Equal(precoSemDesconto, oferta.Preco, 0.001);
        }
    }
}
