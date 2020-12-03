using System.Linq;
using api.CustomExceptions;
using api.Model;

namespace api.Repository
{
    public class CorrentistaRepository : ICorrentistaRepository
    {
        private readonly DataBaseContext _context;

        public CorrentistaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Insert(Correntista correntista)
        {
            _context.Correntistas.Add(correntista);
            _context.SaveChanges();
        }

        public void update(Correntista correntista)
        {
            _context.Correntistas.Update(correntista);
            _context.SaveChanges();
        }

        public Conta GetAccountById(int accountNumber)
        {
            return _context.Contas.FirstOrDefault(f => f.NumeroConta == accountNumber);
        }

        public Correntista GetByCPF(string cpf)
        {
            Correntista registro = _context.Correntistas.SingleOrDefault(f => f.CPF == cpf);
            
            if (registro == null)
                throw new RecordNotFoundException();
            
            return registro;
        }
    }
}