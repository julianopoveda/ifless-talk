using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Model
{
    public class Correntista
    {
        private static int fakeId = 1;

        [Key]
        public int ID { get; set;}
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public Correntista(string cpf, string nome, string endereco)
        {
            ID = fakeId++;
            CPF = cpf;
            Nome = nome;
            Endereco = endereco;
        }

        protected Correntista(){}

        public static Correntista NovaCorrentista(string cpf, string nome, string endereco)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(endereco))
                throw new ArgumentException("cpf, nome e endereço são campos obrigatórios");

            return new Correntista(cpf, nome, endereco);
        }

        public (Correntista correntista, List<string> erros) AtualizarDadosCadastrais(Correntista dadosAtualizados)
        {
            List<string> erros = new List<string>();

            if (dadosAtualizados.Nome != Nome)
                erros.Add("O nome não pode ser alterado");

            Endereco = dadosAtualizados.Endereco;

            return (this, erros);
        }
    }
}