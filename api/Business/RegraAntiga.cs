using System;

namespace api.Business
{
    public class RegraAntiga : IRegraJuros
    {
        public decimal AtualizarSaldo(decimal saldoAtual, DateTime dataAniversario)
        {
            int periodoMeses = (DateTime.Today.Subtract(dataAniversario).Days) / 30;
            return saldoAtual * (decimal)Math.Pow(1 + 0.005, periodoMeses);
        }
    }
}