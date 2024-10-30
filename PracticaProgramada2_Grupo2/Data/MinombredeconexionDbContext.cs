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
        public DbSet<CancionModel> g2_canciones { get; set; }
        public DbSet<PlaylistModel> g2_playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().ToTable("g2_usuarios");
            modelBuilder.Entity<CancionModel>().ToTable("g2_canciones");
            modelBuilder.Entity<PlaylistModel>().ToTable("g2_playlists");
        }
    }
}
