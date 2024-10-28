using PracticaProgramada2_Grupo2.Controllers;
using PracticaProgramada2_Grupo2.Models;
using Microsoft.EntityFrameworkCore;

namespace PracticaProgramada2_Grupo2.Data
{
    public class MinombredeconexionDbContext : DbContext
    {
        public MinombredeconexionDbContext(DbContextOptions<MinombredeconexionDbContext> options) 
        : base(options) { }

        public DbSet<UsuarioModel> g2_usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().ToTable("g2_usuarios");
        }
    }
}
