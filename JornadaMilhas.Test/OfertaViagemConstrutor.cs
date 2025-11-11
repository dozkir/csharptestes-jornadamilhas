using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2025-01-01", "2025-02-01", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2025-01-01", "2025-02-01", 50, true)]
        [InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1, false)]
        [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
        [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
        public void RetornaSeOfertaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            // Padrão AAA (Arrange, Act, Assert)

            // Cenário
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            // Ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Validação
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidosQuandoRotaNula()
        {
            Rota rota = null;
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(12));
            double preco = 100.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaPeriodoInvalidoQuandoPeriodoInvalido()
        {
            // Cenário
            Periodo periodo = new Periodo(DateTime.Now.AddDays(12), DateTime.Now);
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            double preco = 100.0;

            // Ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Validacao
            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", periodo.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void RetornaPrecoInvalidoQuandoPrecoMenorOuIgualAZero(double preco)
        {
            // Arrange
            Rota rota = new Rota("Origem", "Destino");
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(12));

            // Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {
            // Arrange
            Periodo periodo = new Periodo(DateTime.Now.AddDays(12), DateTime.Now);
            Rota rota = null;
            double preco = -100;

            // Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            int quantidadeEsperada = 3;

            // Assert
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }
    }
}