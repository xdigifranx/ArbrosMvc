using Microsoft.EntityFrameworkCore;
using Arbros.Shared.Models;

namespace Arbros.Server
{
    public class AplicacionDbContex : DbContext
    {
        public AplicacionDbContex(DbContextOptions<AplicacionDbContex> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Paises> Paises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Asegúrate de llamar al método base

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.Pais)             // Persona tiene un Pais
                .WithMany(p => p.Personas)      // Pais tiene muchas Personas
                .HasForeignKey(p => p.PaisId);  // Clave foránea en Persona
        }
    }
}
