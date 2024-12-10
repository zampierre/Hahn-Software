using Microsoft.EntityFrameworkCore;
using WeatherApi.Data; // Substitua pelo namespace do seu WeatherDbContext

var builder = WebApplication.CreateBuilder(args);

// Configura��o da string de conex�o
var connectionString = builder.Configuration.GetConnectionString("WeatherDb");

// Adicionando o DbContext ao cont�iner de servi�os
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlServer(connectionString)); // Substitua pelo provedor que voc� est� usando (ex: UseSqlite, UseMySql, etc.)

// Adicionar servi�os ao cont�iner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura��o do pipeline de requisi��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
