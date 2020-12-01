using System;

namespace api.Business
{
    public interface IRegraJuros
    {
        decimal AtualizarSaldo(decimal saldoAtual, DateTime dataAniversario);
    }
}