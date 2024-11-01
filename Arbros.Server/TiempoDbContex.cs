using Microsoft.EntityFrameworkCore;
using Arbros.Shared.Models;

public class TiempoDbContex : DbContext
{
	public TiempoDbContex(DbContextOptions<TiempoDbContex> options) : base(options) { }

	public DbSet<Paises> Paises { get; set; }
	public DbSet<Persona> Personas { get; set; }
	public DbSet<Tiempo> Tiempo { get; set; } // Asegúrate de que esto esté correcto

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Persona>()
			.HasOne(p => p.Pais)
			.WithMany(p => p.Personas)
			.HasForeignKey(p => p.PaisId);

		modelBuilder.Entity<Tiempo>()
			.HasOne(t => t.Persona)
			.WithMany(p => p.Tiempos)
			.HasForeignKey(t => t.PersonaId);
	}
}


