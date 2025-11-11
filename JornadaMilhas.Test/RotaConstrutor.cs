using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class RotaConstrutor
    {
        [Fact]
        public void RetornaRotaValidaQuandoDadosSaoValidos()
        {
            Rota rota = new Rota("Rio de Janeiro", "São Paulo");
            Assert.True(rota.EhValido);
        }

        [Fact]
        public void RetornaRotaInvalidaQuandoDadosSaoNulos()
        {
            // Arrange
            int quantidadeErrosEsperada = 2;

            // Act
            Rota rota = new Rota(null, null);

            // Assert
            Assert.Equal(quantidadeErrosEsperada, rota.Erros.Count());
        }
    }
}
