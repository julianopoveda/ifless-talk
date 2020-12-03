using System;
using System.ComponentModel.DataAnnotations;

namespace api.Model
{
    public abstract class Conta
    {
        [Key]
        public int NumeroConta { get; set; }
        public Correntista Correntista { get; set; }
        public decimal Saldo { get; set; }

        protected Conta() { }

        public abstract decimal EfetuarDeposito(DateTime dataDeposito, decimal valor);
    }

    public class ContaCorrente : Conta
    {
        public bool AplicarTaxas { get; set; }

        protected ContaCorrente() { }

        public static ContaCorrente NovaContaCorrente(Correntista correntista, decimal saldoInicial, bool aplicarTaxas)
        {
            int numeroConta = correntista.CPF.GetHashCode();

            return new ContaCorrente(correntista, numeroConta, saldoInicial, aplicarTaxas);
        }

        //O correto deste método seria gerar uma movimentação financeira, porém por questões de simplificação não criei essa classe
        public override decimal EfetuarDeposito(DateTime dataDeposito, decimal valor)
        {
            if (AplicarTaxas)
                valor = valor * (1 - 0.005M);

            Saldo += valor;
            return Saldo;
        }

        public ContaCorrente(Correntista correntista, int numeroConta, decimal saldoInicial, bool aplicarTaxas)
        {
            Correntista = correntista;
            NumeroConta = numeroConta;
            Saldo = saldoInicial;
            AplicarTaxas = aplicarTaxas;
        }
    }

    public class ContaPoupanca : Conta
    {
        private static DateTime dataCorteRegraNova = new DateTime(2010, 01, 01);

        public DateTime DataPrimeiroDeposito { get; set; }

        public Business.IRegraJuros Regra { get; set; }

        protected ContaPoupanca() { }
        public ContaPoupanca(Correntista correntista, int numeroConta, decimal saldo, DateTime dataPrimeiroDeposito, Business.IRegraJuros regra)
        {
            Correntista = correntista;
            NumeroConta = numeroConta;
            Saldo = saldo;
            DataPrimeiroDeposito = dataPrimeiroDeposito;
            Regra = regra;

        }

        public static ContaPoupanca NovaConta(Correntista correntista, decimal saldoInicial)
        {
            DateTime dataPrimeiroDeposito = DateTime.MinValue;

            if (saldoInicial > 0)
                dataPrimeiroDeposito = DateTime.Today;

            Business.IRegraJuros regra = DefinirRegra();

            int numeroConta = correntista.CPF.GetHashCode();

            return new ContaPoupanca(correntista, numeroConta, saldoInicial, dataPrimeiroDeposito, regra);
        }

        private static Business.IRegraJuros DefinirRegra(DateTime? dataPrimeiroDeposito = null)
        {
            if (dataPrimeiroDeposito.GetValueOrDefault(DateTime.MinValue) > DateTime.MinValue && dataPrimeiroDeposito.GetValueOrDefault(DateTime.MinValue) <= dataCorteRegraNova)
                return new Business.RegraAntiga();
            else if (dataPrimeiroDeposito.GetValueOrDefault(DateTime.MinValue) > DateTime.MinValue && dataPrimeiroDeposito.GetValueOrDefault(DateTime.MinValue) > dataCorteRegraNova)
                return new Business.RegraNova();

            return null;
        }


        public decimal CalcularValorPoupanca()
        {
            decimal saldoAtualizado = 0;

            if (DataPrimeiroDeposito == DateTime.MinValue)
                return 0;
            else if (DataPrimeiroDeposito > DateTime.MinValue && DataPrimeiroDeposito <= dataCorteRegraNova)
                saldoAtualizado = new Business.RegraAntiga().AtualizarSaldo(Saldo, DataPrimeiroDeposito);
            else
                saldoAtualizado = new Business.RegraNova().AtualizarSaldo(Saldo, DataPrimeiroDeposito);

            return saldoAtualizado;
        }

        public decimal CalcularValorPoupancaIfless()
        {
            if (DataPrimeiroDeposito == DateTime.MinValue)
                return 0;

            return Regra.AtualizarSaldo(Saldo, DataPrimeiroDeposito);
        }

        public override decimal EfetuarDeposito(DateTime dataDeposito, decimal valor)
        {
            if (DataPrimeiroDeposito == DateTime.MinValue)
            {
                DataPrimeiroDeposito = dataDeposito;
                Regra = DefinirRegra(dataDeposito);
            }

            Saldo += valor;

            return Saldo;
        }
    }
}