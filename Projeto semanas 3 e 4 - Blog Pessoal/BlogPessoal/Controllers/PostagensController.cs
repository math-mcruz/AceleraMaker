using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;
using BlogPessoal.Repositories.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> Get()
    {
        var postagens = await _uof.PostagemRepository.GetAllAsync();

        if (postagens is null)
            return NotFound("Não existem postagens criados");
        
        var postResponseDTO = postagens.ToPostagemDTOList();

        return Ok(postResponseDTO);
    }

    [HttpGet("filtro")]
    public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> GetFiltro([FromQuery] PostagensFiltroAutorTema postFiltro)
    {
        var postagens = await _uof.PostagemRepository.GetFiltroAutorTemaAsync(postFiltro);

        if (postagens is null)
            return NotFound("Não existem postagens criados");

        var metadata = new
        {
            postagens.TotalCount,
            postagens.PageSize,
            postagens.CurrentPage,
            postagens.TotalPages,
            postagens.HasNext,
            postagens.HasPrevious,
        };
        //teve que instalar Newtonsoft.json da aula. considerar se vai usar -----------------------------------------************************
        Response.Headers.Append("Pagination", JsonConvert.SerializeObject(metadata));

        var postResponseDto = postagens.ToPostagemDTOList();
        
        return Ok(postagens);
    }

    [HttpPost]
    public async Task<ActionResult<PostagemRequestDTO>> Post(PostagemRequestDTO postRequestDto)
    {
        if (postRequestDto is null)
            return BadRequest("Dados inválidos");

        var post = postRequestDto.RequestToPost();

        var postCriado = _uof.PostagemRepository.Create(post);
        await _uof.CommitAsync();

        var postResponseDto = postCriado.ToPostResponseDTO();

        return StatusCode(StatusCodes.Status201Created, postResponseDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PostagemRequestDTO>> Put(int id, PostagemResponseDTO postResponseDto)
    {
        if (id != postResponseDto.PostagemId)
            return BadRequest("Dados inválidos");

        var post = postResponseDto.ResponseToPost();

        var postAtualizado = _uof.PostagemRepository.Update(post);
        await _uof.CommitAsync();

        var novoPostResponseDto = postAtualizado.ToPostResponseDTO();

        return Ok(novoPostResponseDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PostagemResponseDTO>> Delete(int id)
    {
        var post = await _uof.PostagemRepository.GetAsync(c => c.PostagemId == id);
        if (post is null)
            return NotFound("Postagem não encontrado");


        var postExcluido = _uof.PostagemRepository.Delete(post);
        await _uof.CommitAsync();

        var postResponseDto = postExcluido.ToPostResponseDTO();

        return Ok(postResponseDto);
    }
}