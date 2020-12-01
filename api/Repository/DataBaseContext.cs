using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DataBaseContext:DbContext
    {
        public DbSet<Correntista> Correntistas{ get; set; }
        public DbSet<api.Model.Conta> Contas { get; set; }
    }
}