using BlogPessoal.Models;
using BlogPessoal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostagensController : ControllerBase
{
    //requisitos funcionais: Criação de novas postagens vinculadas a um usuário e a um tema, Atualização e exclusão de postagens existentes e
    //Listagem de todas as postagens com suporte a filtros:
    //Por tema.
    //Por autor.
    
    
    private readonly IUnitOfWork _uof;

    public PostagensController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Postagem>> Get()
    {
        var postagem = _uof.PostagemRepository.GetAll();
        return Ok(postagem);
    }

    [HttpPost]
    public ActionResult Post(Postagem postagem)
    {
        if (postagem is null)
        {
            return BadRequest("Dados inválidos");
        }
        var novaPostagem = _uof.PostagemRepository.Create(postagem);
        _uof.Commit();

        return StatusCode(StatusCodes.Status201Created, novaPostagem);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Postagem postagem)
    {
        if (id != postagem.PostagemId)
        {
            return BadRequest("Dados inválidos");
        }

        _uof.PostagemRepository.Update(postagem);
        _uof.Commit();

        return Ok(postagem);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var postagem = _uof.PostagemRepository.Get(c => c.PostagemId == id);
        if (postagem is null)
        {
            return NotFound("Postagem não encontrado");
        }
        var postagemExcluir = _uof.PostagemRepository.Delete(postagem);
        _uof.Commit();

        return Ok(postagemExcluir);
    }
}
