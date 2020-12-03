using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Model
{
    public class CorrentistaDTO //: IValidatableObject uncomment this to validate a model
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(CPF) && !string.IsNullOrEmpty(Endereco);
        }
        
        public Correntista ToCorrentista() => new Correntista(CPF, Nome, Endereco);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validations = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Nome))
                validations.Add(new ValidationResult("nome é obrigatório"));
            if (string.IsNullOrEmpty(CPF))
                validations.Add(new ValidationResult("CPF é obrigatório"));

            else if (!(CPF.Length == 11 || CPF.Length == 14))
                validations.Add(new ValidationResult("CPF deve ter 11 caractéres sem máscara ou 14 com máscara"));

            if (string.IsNullOrEmpty(Endereco))
                validations.Add(new ValidationResult("correntista sem endereço não é possível"));

            return validations;
        }
    }
}