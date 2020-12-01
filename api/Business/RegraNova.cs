using System;

namespace api.Business
{
    public class RegraNova : IRegraJuros
    {
        private double Selic70 { get; set; }
        private double TaxaReferencial { get; set; }

        public RegraNova()
        {
            Selic70 = (0.0012705 * 0.7) / 12.0;
            TaxaReferencial = 0.17;
        }

        public decimal AtualizarSaldo(decimal saldoAtual, DateTime dataAniversario)
        {
            int periodoMeses = (DateTime.Today.Subtract(dataAniversario).Days) / 30;
            return saldoAtual * (decimal)Math.Pow(1 + (double)(Selic70 + TaxaReferencial), periodoMeses);
        }
    }
}