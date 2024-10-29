using Microsoft.EntityFrameworkCore;
using Arbros.Shared.Models;

public class TiempoDbContex : DbContext
{
    public TiempoDbContex(DbContextOptions<TiempoDbContex> options) : base(options) { }

    public DbSet<Tiempo> Tiempo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tiempo>().ToTable("Tiempo");
    }
}
