using System;

namespace api.Model
{
    public class Correntista
    {        
        private static int fakeId = 1;
        public int ID { get; set; }
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

        public static Correntista NovaCorrentista(string cpf, string nome, string endereco)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(endereco))
                throw new ArgumentException("cpf, nome e endereço são campos obrigatórios");

            return new Correntista(cpf, nome, endereco);
        }
    }
}