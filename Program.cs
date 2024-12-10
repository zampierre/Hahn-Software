using Microsoft.EntityFrameworkCore;
using WeatherApi.Data; // Substitua pelo namespace do seu WeatherDbContext

var builder = WebApplication.CreateBuilder(args);

// Configuração da string de conexão
var connectionString = builder.Configuration.GetConnectionString("WeatherDb");

// Adicionando o DbContext ao contêiner de serviços
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlServer(connectionString)); // Substitua pelo provedor que você está usando (ex: UseSqlite, UseMySql, etc.)

// Adicionar serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
