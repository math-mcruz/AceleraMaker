using Microsoft.EntityFrameworkCore;
using BlogPessoal.Data;
using System.Text.Json.Serialization;
using BlogPessoal.Middlewares.Filters;
using BlogPessoal.Middlewares.Exceptions;
using BlogPessoal.Repositories;
using BlogPessoal.Repositories.Postagens;
using BlogPessoal.Repositories.Usuarios;
using BlogPessoal.Repositories.Temas;
using BlogPessoal.Repositories.UnitsOfWork;

var builder = WebApplication.CreateBuilder(args);

//ajuste para ignorar ciclos
//ajuste para colocar o filtro global nos controladores
builder.Services.AddControllers(options => { options.Filters.Add(typeof(ApiExceptionFilter)); } ).AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);




builder.Services.AddOpenApi();




//variavel de ambiente para proteger a senha do Banco de Dados
//temporario achar uma solução melhor
string? senhaBanco = Environment.GetEnvironmentVariable("SENHA_BANCO_LOCAL");

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

string? connection = $"{mySqlConnection};Pwd={senhaBanco};";

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));






//Aplicando o filtro
builder.Services.AddScoped<ApiLoggingFilter>();




//fazer isso para cada repository
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<IPostagemRepository,PostagemRepository>();
builder.Services.AddScoped<ITemaRepository,TemaRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ConfigureExceptionHandler();

    //lembrar que isso é só para o modo Development, pois não pode mostrar o stacktrace no modo deploy, por segurança
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
