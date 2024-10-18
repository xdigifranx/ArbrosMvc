using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Arbros.Server
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// Configuración de la base de datos
			services.AddDbContext<AplicacionDbContex>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<UsuariosDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("UsuariosConnection")));
            // Agregar CORS para permitir solicitudes desde Blazor WebAssembly
            services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins",
					builder => builder.AllowAnyOrigin()
									  .AllowAnyMethod()
									  .AllowAnyHeader());
			});

			// Agregar controladores (API)
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}


			// Usar CORS
			app.UseCors("AllowAllOrigins");

			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers(); // Mapeo de controladores API
			});
		}
	}
}


