using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
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
        if (temas is null)
            return NotFound("Não existem temas criados");

        var temasDto = temas.ToTemaDTOList();

        return Ok(temasDto);
    }

    [HttpPost]
    public ActionResult<TemaDTO> Post(TemaDTO temaDto)
    {
        if(temaDto is null)
            return BadRequest("Dados inválidos");
        
        var tema = temaDto.ToTema();

        var temaCriado = _uof.TemaRepository.Create(tema);
        _uof.Commit();//salva no banco

        var novoTemaDTO = temaCriado.ToTemaDTO();

        return StatusCode(StatusCodes.Status201Created, novoTemaDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<TemaDTO> Put(int id, TemaDTO temaDto)
    {
        if (id != temaDto.TemaId)
            return BadRequest("Dados inválidos");

        var tema = temaDto.ToTema();

        var temaAtualizado = _uof.TemaRepository.Update(tema);
        _uof.Commit();

        var novoTemaDto = temaAtualizado.ToTemaDTO();

        return Ok(novoTemaDto);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<TemaDTO> Delete(int id)
    {
        var tema = _uof.TemaRepository.Get(c=>c.TemaId == id);

        if(tema is null)
            return NotFound("Tema não encontrado");

        var temaExcluido = _uof.TemaRepository.Delete(tema);
        _uof.Commit();

        var temaExcluidoDto = temaExcluido.ToTemaDTO();

        return Ok(temaExcluidoDto);
    }
}
