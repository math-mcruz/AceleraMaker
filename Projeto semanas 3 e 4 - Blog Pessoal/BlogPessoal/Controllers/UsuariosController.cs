using BlogPessoal.Data;
using BlogPessoal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    //requisitos funcionais: Cadastro de usuarios, Atualização de dados dos usuarios e Exclusão de usuarios 
    private readonly IUnitOfWork _uof;

    public UsuariosController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    //[HttpGet] ai segue os get, put, post e delete
}
