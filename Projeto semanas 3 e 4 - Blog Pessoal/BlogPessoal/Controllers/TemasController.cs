using BlogPessoal.DTOs;
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

    private readonly IUnitOfWork _uof;

    public TemasController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TemaDTO>> Get()
    {
        var temas = _uof.TemaRepository.GetAll();
        return Ok(temas);
    }

    [HttpPost]
    public ActionResult<TemaDTO> Post(TemaDTO tema)
    {
        if(tema is null)
        {
            return BadRequest("Dados inválidos");
        }
        var novoTema = _uof.TemaRepository.Create(tema);
        _uof.Commit();//salva no banco

        return StatusCode(StatusCodes.Status201Created, novoTema);
    }

    [HttpPut("{id:int}")]
    public ActionResult<TemaDTO> Put(int id, TemaDTO tema)
    {
        if (id != tema.TemaId)
        {
            return BadRequest("Dados inválidos");
        }

        _uof.TemaRepository.Update(tema);
        _uof.Commit();

        return Ok(tema);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<TemaDTO> Delete(int id)
    {
        var tema = _uof.TemaRepository.Get(c=>c.TemaId == id);
        if(tema is null)
        {
            return NotFound("Tema não encontrado");
        }
        var temaExcluir = _uof.TemaRepository.Delete(tema);
        _uof.Commit();

        return Ok(temaExcluir);
    }
}
