using System.Reflection;
using dotnet.Service.Clientes;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { Title = "Cooperativa Financeira Alfa", 
      Version = "v1", 
      Description = "Sistema legado modernizado da Cooperativa.\n\n" +
                      "**Recursos:**\n" +
                      "* Cadastro de clientes.\n" +
                      "* Consulta de clientes.\n" +
                      "* Atualização de Telefone e E-mail de clientes.\n" +
                      "* Remoção de clientes.\n",
      Contact = new OpenApiContact
      {
          Name = "Matheus Cruz",
          Email = "matheusmcruz2004@gmail.com",
          Url = new Uri("https://github.com/math-mcruz")
      }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});    

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();