using System.Linq;
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
            throw new System.NotImplementedException();
        }

        public void update(Correntista correntista)
        {
            throw new System.NotImplementedException();
        }

        public Conta GetAccountById(int accountNumber)
        {
            return _context.Contas.FirstOrDefault(f => f.NumeroConta == accountNumber);
        }
    }
}