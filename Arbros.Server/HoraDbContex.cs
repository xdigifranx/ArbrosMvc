using Microsoft.EntityFrameworkCore;

namespace Arbros.Server
{
	public class HoraDbContex : DbContext
	{
		public HoraDbContex(DbContextOptions<HoraDbContex> options)
			: base(options) 
		{
		}
		public DbSet<HoraDbContex> Tiempo { get; set; }
	}
}
