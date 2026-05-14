using Microsoft.EntityFrameworkCore;
using BlogPessoal.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//ajuste para ignorar ciclos
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddOpenApi();

//variavel de ambiente para proteger a senha do Banco de Dados
//temporario achar uma solução melhor
string? senhaBanco = Environment.GetEnvironmentVariable("SENHA_BANCO_LOCAL");

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

string? connection = $"{mySqlConnection};Pwd={senhaBanco};";

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

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
