using BlogPessoal.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    //requisitos funcionais: Cadastro de usuarios, Atualização de dados dos usuarios e Exclusão de usuarios 
    private readonly BlogDbContext _context;

    public UsuariosController(BlogDbContext context)
    {
        _context = context;
    }

    //[HttpGet] ai segue os get, put, post e delete
}
