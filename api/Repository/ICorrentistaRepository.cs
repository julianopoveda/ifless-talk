using api.Model;

namespace api.Repository
{
    public interface ICorrentistaRepository
    {
         void Insert(Correntista correntista);
         void update(Correntista correntista);

         Conta GetAccountById(int accountNumber);
    }
}