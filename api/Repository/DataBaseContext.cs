using System.Diagnostics.CodeAnalysis;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Correntista> Correntistas{ get; set; }
        public DbSet<api.Model.Conta> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaCorrente>().HasBaseType<Conta>();
            modelBuilder.Entity<ContaPoupanca>().HasBaseType<Conta>().Ignore(p=>p.Regra);

        }
    }
}