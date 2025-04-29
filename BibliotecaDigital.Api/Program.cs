using BibliotecaDigital.Core.Interfaces;
using BibliotecaDigital.Core.Services;
using BibliotecaDigital.Infrastructure.Data;
using BibliotecaDigital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registre o DbContext com a string de conexão adequada
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Ajuste se necessário

// Registre os outros serviços
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

// Adicione outros serviços necessários, como controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configuração de pipeline de middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
