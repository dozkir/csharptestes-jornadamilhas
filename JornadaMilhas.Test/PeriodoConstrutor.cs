using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class PeriodoConstrutor
    {
        [Fact]
        public void RetornaPeriodoInvalidoQuandoPeriodoInvalido()
        {
            Periodo periodo = new Periodo(DateTime.Now.AddDays(12), DateTime.Now);

            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", periodo.Erros.Sumario);
            Assert.False(periodo.EhValido);
        }
    }
}
