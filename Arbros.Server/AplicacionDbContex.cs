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
        }
    }
