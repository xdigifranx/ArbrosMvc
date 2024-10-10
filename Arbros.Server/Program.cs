using Arbros.Server;
using Arbros.Server.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de Razor Components interactivos
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

 builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAnyOrigin",
		builder =>
		{
			builder.AllowAnyOrigin()
				   .AllowAnyMethod()
				   .AllowAnyHeader();
		});
});

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Configurar el DbContext

builder.Services.AddDbContext<AplicacionDbContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Construir la aplicaci�n
var app = builder.Build();

// Configurar el pipeline de solicitudes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAnyOrigin");
app.UseAntiforgery();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Mapear controladores API (si los tienes)
app.MapControllers();
app.Run();
