using Microsoft.EntityFrameworkCore;

namespace Arbros.Server
{
	public class PaisesDbContex: DbContext
	{
		public PaisesDbContex(DbContextOptions<PaisesDbContex> options) 
			: base(options) 
		{
		}
		public DbSet<PaisesDbContex> Paises { get; set; }
	}
}
