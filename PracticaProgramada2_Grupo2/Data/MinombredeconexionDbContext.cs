using PracticaProgramada2_Grupo2.Controllers;
using PracticaProgramada2_Grupo2.Models;
using Microsoft.EntityFrameworkCore;

namespace PracticaProgramada2_Grupo2.Data
{
    public class MinombredeconexionDbContext : DbContext
    {
        public MinombredeconexionDbContext(DbContextOptions<MinombredeconexionDbContext> options) 
        : base(options) { }

        public DbSet<UsuarioModel> G2_Usuarios { get; set; }
        public DbSet<CancionModel> G2_Canciones { get; set; }
        public DbSet<PlaylistModel> G2_Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().ToTable("G2_Usuarios");
            modelBuilder.Entity<CancionModel>().ToTable("G2_Canciones");
            modelBuilder.Entity<PlaylistModel>().ToTable("G2_Playlists");
        }
    }
}
