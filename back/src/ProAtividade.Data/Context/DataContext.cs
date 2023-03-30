

using Microsoft.EntityFrameworkCore;
using ProAtividade.Data.Mappings;
using ProAtividade.Domain.Entities;

namespace ProAtividade.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Atividade> Atividades { get; set; } // itens da Base de Dados

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration(new AtividadeMap());
        }

        
    }
}