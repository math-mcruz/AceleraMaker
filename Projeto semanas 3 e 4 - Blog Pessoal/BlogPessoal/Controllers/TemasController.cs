using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;
using BlogPessoal.Repositories.UnitsOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[Route("api/temas")]
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
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<TemaResponseDTO>>> Get()
    {
        var temas = await _uof.TemaRepository.GetAllAsync();
        if (temas is null || !temas.Any())
            return NotFound("Não existem temas criados");

        var temasResponseDto = temas.ToTemaDTOList();

        return Ok(temasResponseDto);
    }

    [HttpPost]
    [Authorize(Policy = "RequerAdmin")]
    public async Task<ActionResult<TemaResponseDTO>> Post(TemaRequestDTO temaRequestDto)
    {
        if(temaRequestDto is null)
            return BadRequest("Dados inválidos");
        
        var tema = temaRequestDto.RequestToTema();

        var temaCriado = _uof.TemaRepository.Create(tema);
        await _uof.CommitAsync();//salva no banco
        
        var TemaResponseDTO = temaCriado.ToTemaResponseDTO();

        return StatusCode(StatusCodes.Status201Created, TemaResponseDTO);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequerAdmin")]
    public async Task<ActionResult<TemaResponseDTO>> Put(int id, TemaUpdateDTO temaUpdateDto)
    {
        if (id != temaUpdateDto.TemaId)
            return BadRequest("Dados inválidos");

        var temaExiste = await _uof.TemaRepository.GetAsync(t => t.TemaId == id);

        if (temaExiste is null) 
            return NotFound("Tema não encontrado.");

        var tema = temaUpdateDto.UpdateToTema();

        var temaAtualizado = _uof.TemaRepository.Update(tema);
        await _uof.CommitAsync();

        var novoTemaResponseDto = temaAtualizado.ToTemaResponseDTO();

        return Ok(novoTemaResponseDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "RequerAdmin")]
    public async Task<ActionResult<TemaResponseDTO>> Delete(int id)
    {
        var tema = await _uof.TemaRepository.GetAsync(c=>c.TemaId == id);

        if(tema is null)
            return NotFound("Tema não encontrado");

        var temaExcluido = _uof.TemaRepository.Delete(tema);
        await _uof.CommitAsync();

        return NoContent();
    }
}
