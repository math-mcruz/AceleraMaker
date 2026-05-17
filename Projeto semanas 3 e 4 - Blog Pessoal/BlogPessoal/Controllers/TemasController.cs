using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;
using BlogPessoal.Repositories.UnitsOfWork;
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
    public ActionResult<IEnumerable<TemaResponseDTO>> Get()
    {
        var temas = _uof.TemaRepository.GetAll();
        if (temas is null)
            return NotFound("Não existem temas criados");

        var temasResponseDto = temas.ToTemaDTOList();

        return Ok(temasResponseDto);
    }

    [HttpPost]
    public ActionResult<TemaRequestDTO> Post(TemaRequestDTO temaRequestDto)
    {
        if(temaRequestDto is null)
            return BadRequest("Dados inválidos");
        
        var tema = temaRequestDto.RequestToTema();

        var temaCriado = _uof.TemaRepository.Create(tema);
        _uof.Commit();//salva no banco
        
        var TemaResponseDTO = temaCriado.ToTemaResponseDTO();

        return StatusCode(StatusCodes.Status201Created, TemaResponseDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<TemaRequestDTO> Put(int id, TemaResponseDTO temaResponseDto)
    {
        if (id != temaResponseDto.TemaId)
            return BadRequest("Dados inválidos");

        var tema = temaResponseDto.ResponseToTema();

        var temaAtualizado = _uof.TemaRepository.Update(tema);
        _uof.Commit();

        var novoTemaResponseDto = temaAtualizado.ToTemaResponseDTO();

        return Ok(novoTemaResponseDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<TemaResponseDTO> Delete(int id)
    {
        var tema = _uof.TemaRepository.Get(c=>c.TemaId == id);

        if(tema is null)
            return NotFound("Tema não encontrado");

        var temaExcluido = _uof.TemaRepository.Delete(tema);
        _uof.Commit();

        var temaResponseDto = temaExcluido.ToTemaResponseDTO();

        return Ok(temaResponseDto);
    }
}
