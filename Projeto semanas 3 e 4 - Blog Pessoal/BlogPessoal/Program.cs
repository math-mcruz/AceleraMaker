using BlogPessoal.Config.Data;
using BlogPessoal.Config.RateLimitConfig;
using BlogPessoal.Data;
using BlogPessoal.Middlewares.Exceptions;
using BlogPessoal.Models;
using BlogPessoal.Repositories.GenericRepository;
using BlogPessoal.Repositories.Postagens;
using BlogPessoal.Repositories.Temas;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services;
using BlogPessoal.Services.IA;
using BlogPessoal.Services.Postagens;
using BlogPessoal.Services.Tema;
using BlogPessoal.Services.Temas;
using BlogPessoal.Services.Token;
using BlogPessoal.Services.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

//ajuste para ignorar ciclos
//ajuste para colocar o filtro global nos controladores
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { Title = "Blog Pessoal", 
      Version = "v1", 
      Description = "API RESTful desenvolvida em ASP.NET Core para gerenciamento de um Blog Pessoal.\n\n" +
                      "**Principais Recursos:**\n" +
                      "* Autenticação e Autorização com JWT.\n" +
                      "* Gestão de Usuários e Perfis (Admin e Usuário).\n" +
                      "* Operações completas de CRUD para Temas e Postagens.\n",
      Contact = new OpenApiContact
      {
          Name = "Matheus Cruz",
          Email = "matheusmcruz2004@gmail.com",
          Url = new Uri("https://github.com/math-mcruz")
      }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT:",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http, //para não precisar escrever Bearer e depois o token
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    //filtro automatico
    c.OperationFilter<Swashbuckle.AspNetCore.Filters.SecurityRequirementsOperationFilter>();
});

//autenticação
builder.Services.AddControllers();

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

//Autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequerAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequerUsuario", policy => policy.RequireRole("Usuario", "Admin"));
});

//aplicando Rate Limiting para não sobrecarregar o blog com requisições

var myOptions = new RateLimitOptions();
var myOptionsGlobal = new RateLimitGlobalOptions();

builder.Configuration.GetSection(RateLimitOptions.MyRateLimit).Bind(myOptions);
builder.Configuration.GetSection(RateLimitGlobalOptions.RateLimitGlobal).Bind(myOptionsGlobal);

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddSlidingWindowLimiter("sliding", regras =>
    {
        regras.PermitLimit = myOptions.PermitLimit;
        regras.Window = TimeSpan.FromSeconds(myOptions.Window);
        regras.SegmentsPerWindow = myOptions.SegmentsPerWindow;
        regras.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        regras.QueueLimit = myOptions.QueueLimit;
    });

    options.AddSlidingWindowLimiter("global", regras =>
    {
        regras.PermitLimit = myOptionsGlobal.PermitLimit;
        regras.Window = TimeSpan.FromMinutes(myOptionsGlobal.Window);
        regras.SegmentsPerWindow = myOptionsGlobal.SegmentsPerWindow;
        regras.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        regras.QueueLimit = myOptionsGlobal.QueueLimit;
    });
});

//Repository
builder.Services.AddScoped<IPostagemRepository,PostagemRepository>();
builder.Services.AddScoped<ITemaRepository,TemaRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IPostagemService, PostagemService>();
builder.Services.AddHttpClient<GeminiService>();
builder.Services.AddScoped<IIAService, IAService>();

//---------------------------------------------------------------------------------------------------------------------------
//                                                          Build
//---------------------------------------------------------------------------------------------------------------------------

var app = builder.Build();

app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Pessoal API"));
}
app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("global");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbInitializer.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        // Se der algum erro de conexão com banco, ele avisa no terminal
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um erro ocorreu ao popular o banco de dados.");
    }
}

app.Run();