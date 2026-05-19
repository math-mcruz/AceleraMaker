using BlogPessoal.Data;
using BlogPessoal.Middlewares.Exceptions;
using BlogPessoal.Middlewares.Filters;
using BlogPessoal.Models;
using BlogPessoal.Repositories.GenericRepository;
using BlogPessoal.Repositories.Postagens;
using BlogPessoal.Repositories.Temas;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

//ajuste para ignorar ciclos
//ajuste para colocar o filtro global nos controladores
builder.Services.AddControllers(options => { options.Filters.Add(typeof(ApiExceptionFilter)); } ).AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogPessoal", Version = "v1" });
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT:\n(Bearer token)",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    //filtro automatico
    c.OperationFilter<Swashbuckle.AspNetCore.Filters.SecurityRequirementsOperationFilter>();
});

//autenticação
builder.Services.AddControllers();
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();

//configurando o Identity
builder.Services.AddIdentity<Usuario, IdentityRole<int>>().AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<BlogDbContext>().AddDefaultTokenProviders();

//variavel de ambiente para proteger a senha do Banco de Dados
//temporario achar uma solução melhor
string? senhaBanco = Environment.GetEnvironmentVariable("SENHA_BANCO_LOCAL");

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

string? connection = $"{mySqlConnection};Pwd={senhaBanco};";


//configurando validação por token JWT
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Chave secreta inválida");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; //quando for para produção é bom ativar
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});




//Aplicando o filtro
builder.Services.AddScoped<ApiLoggingFilter>();


//fazer isso para cada repository ---------------------------------------------*********** falta o de usuario
builder.Services.AddScoped<IPostagemRepository,PostagemRepository>();
builder.Services.AddScoped<ITemaRepository,TemaRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITokenService, TokenService>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.ConfigureExceptionHandler();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Pessoal API"));

    //lembrar que isso é só para o modo Development, pois não pode mostrar o stacktrace no modo deploy, por segurança
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
