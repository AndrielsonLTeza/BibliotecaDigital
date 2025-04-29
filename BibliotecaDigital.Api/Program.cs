using Microsoft.EntityFrameworkCore;
using BibliotecaDigital.Infrastructure.Data;
using BibliotecaDigital.Core.Interfaces;
using BibliotecaDigital.Core.Services;
using BibliotecaDigital.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure o DbContext para PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registre os repositórios e serviços
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

var app = builder.Build();

// Configure o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();