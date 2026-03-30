using Microsoft.EntityFrameworkCore;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Infrastructure.Data;
using VentasLimpieza.Infrastructure.Mapping;
using VentasLimpieza.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddAutoMapper(typeof(VentasLimpiezaProfile).Assembly);

builder.Services.AddOpenApi();

#region Configurar la BD MySql
var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
builder.Services.AddDbContext<VentasLimpiezaContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
#endregion

builder.Services.AddTransient<IUsuarioRepository,UsuarioRepository >();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
