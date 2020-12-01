using System;

namespace api.Model
{
    public class CorrentistaDTO
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }


        public bool IsValid()
        {
            return string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(CPF) || string.IsNullOrEmpty(Endereco);
        }
    }
}