using BlogPessoal.Models;
using BlogPessoal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TemasController : ControllerBase
{
    //requisitos funcionais: Criação de novos temas, Atualização e exclusão de temas existentes e Listagem de todos os temas

    private readonly ITemaRepository _repository;

    public TemasController(ITemaRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Tema>> Get()
    {
        var temas = _repository.listarTemas();
        return Ok(temas);
    }

    [HttpPost]
    public ActionResult Post(Tema tema)
    {
        if(tema is null)
        {
            return BadRequest("Dados inválidos");
        }
        var novoTema = _repository.CriarTema(tema);
        return new CreatedAtRouteResult("ObterTema", new { id = novoTema.TemaId }, novoTema);//não entendi direito esse ObterTema do video, pesquisar sobre
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Tema tema)
    {
        if (id != tema.TemaId)
        {
            return BadRequest("Dados inválidos");
        }

        _repository.AtualizarTema(tema);
        return Ok(tema);

    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var temaExcluido = _repository.DeletarTema(id);
        if(temaExcluido is null)
        {
            return BadRequest("Dados inválidos");
        }
        return Ok(temaExcluido);

    }
}
