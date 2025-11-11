using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhasV1.Modelos;
using JornadaMilhasV1.Gerenciador;
using Bogus;

namespace JornadaMilhas.Test
{
    public class GerenciadorDeOfertasRecuperaMaiorDesconto
    {
        [Fact]
        public void RetornaOfertaNulaQuandoListaVazia()
        {
            // Arrange
            var listaOfertas = new List<OfertaViagem>(); // A lista tá vazia
            var gerenciador = new GerenciadorDeOfertas(listaOfertas);
            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino == "São Paulo";

            // Act
            var ofertaEncontrada = gerenciador.RecuperaMaiorDesconto(filtro);

            // Assert
            Assert.Null(ofertaEncontrada); // Tem que tá nulo pois a lista de ofertas está vazia.
        }

        [Fact]
        public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
        {
            // São Paulo
            // Valor original: 80
            // Desconto: 40 reais
            // Valor esperado: 40

            // Arrange

            var fakerPeriodo = new Faker<Periodo>()
                .CustomInstantiator(f =>
                {
                    DateTime dataInicio = f.Date.Soon(); // Gerando data que esteja próxima
                    return new Periodo(dataInicio, dataInicio.AddDays(30));
                });

            var rota = new Rota("Curitiba", "São Paulo");

            var fakerOferta = new Faker<OfertaViagem>()
                .CustomInstantiator(f => new OfertaViagem(
                    rota,
                    fakerPeriodo.Generate(),
                    100 * f.Random.Int(1, 100))
                )
                .RuleFor(o => o.Desconto, f => 40)
                .RuleFor(o => o.Ativa, f => true);

            var ofertaEscolhida = new OfertaViagem(rota, fakerPeriodo.Generate(), 80)
            {
                Desconto = 40,
                Ativa = true
            };

            var ofertaInativa = new OfertaViagem(rota, fakerPeriodo.Generate(), 70)
            {
                Desconto = 40,
                Ativa = false
            };

            //var listaOfertas = new List<OfertaViagem>();
            var listaOfertas = fakerOferta.Generate(200);
            listaOfertas.Add(ofertaEscolhida);
            listaOfertas.Add(ofertaInativa);

            var gerenciador = new GerenciadorDeOfertas(listaOfertas);

            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino == "São Paulo";

            double precoEsperado = 40;

            // Act
            var ofertaEncontrada = gerenciador.RecuperaMaiorDesconto(filtro);

            // Assert
            Assert.NotNull(ofertaEncontrada);
            Assert.Equal(precoEsperado, ofertaEncontrada.Preco, 0.0001);
        }
    }
}
