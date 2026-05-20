using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;
using BlogPessoal.Repositories.UnitsOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace BlogPessoal.Controllers;

[Route("api/postagens")]
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

    //todos podem acessar
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> Get()
    {
        var postagens = await _uof.PostagemRepository.GetAllAsync();

        if (postagens is null || !postagens.Any())
            return NotFound("Não existem postagens criados");

        var postResponseDTO = postagens.ToPostagemDTOList();

        return Ok(postResponseDTO);
    }

    [HttpGet("filtro")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> GetFiltro([FromQuery] PostagensFiltroAutorTema postFiltro)
    {
        var postagens = await _uof.PostagemRepository.GetFiltroAutorTemaAsync(postFiltro);

        if (postagens is null || !postagens.Any())
            return NotFound("Não existem postagens criadas com o filtro aplicado.");

        var postResponseDto = postagens.ToPostagemDTOList();

        var resposta = new
        {
            Dados = postResponseDto,
            Paginacao = new
            {
                postagens.TotalCount,
                postagens.PageSize,
                postagens.CurrentPage,
                postagens.TotalPages,
                postagens.HasNext,
                postagens.HasPrevious
            }
        };
        return Ok(resposta);
    }

    [HttpPost]
    [Authorize(Policy = "RequerUsuario")]
    public async Task<ActionResult<PostagemResponseDTO>> Post(PostagemRequestDTO postRequestDto)
    {
        if (postRequestDto is null)
            return BadRequest("Dados inválidos");

        var claimValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userLogadoId))
            return Unauthorized("ID do usuário inválido ou não encontrado no token.");

        var post = postRequestDto.RequestToPost(userLogadoId);

        var postCriado = _uof.PostagemRepository.Create(post);
        await _uof.CommitAsync();

        var postCompleto = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == postCriado.PostagemId);

        var postResponseDto = postCompleto.ToPostResponseDTO();

        return StatusCode(StatusCodes.Status201Created, postResponseDto);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequerUsuario")]
    public async Task<ActionResult<PostagemResponseDTO>> Put(int id, PostagemUpdateDTO postUpdateDto)
    {
        if (id != postUpdateDto.PostagemId)
            return BadRequest("Dados inválidos");

        var claimValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userLogadoId))
            return Unauthorized("ID do usuário inválido ou não encontrado no token.");

        //busca a postagem no banco
        var post = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == id);

        if (post is null)
            return NotFound("Postagem não encontrada.");

        bool ehAdmin = User.IsInRole("Admin");

        //se for adimin ja da falso, e se for o post do autor da falso
        if (post.UsuarioId != userLogadoId && !ehAdmin)
            return Forbid("Sem permissão para editar a postagem.");

        post.UpdateToPost(postUpdateDto);

        var postAtualizado = _uof.PostagemRepository.Update(post);
        await _uof.CommitAsync();

        var postCompleto = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == postAtualizado.PostagemId);

        var novoPostResponseDto = postCompleto.ToPostResponseDTO();

        return Ok(novoPostResponseDto);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PostagemResponseDTO>> Delete(int id)
    {
        var claimValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userLogadoId))
            return Unauthorized("ID do usuário inválido ou não encontrado no token.");

        var post = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == id);

        if (post is null)
            return NotFound("Postagem não encontrado");

        bool ehAdmin = User.IsInRole("Admin");

        if (post.UsuarioId != userLogadoId && !ehAdmin)
            return Forbid("Sem permissão para excluir a postagem.");

        var postExcluido = _uof.PostagemRepository.Delete(post);
        await _uof.CommitAsync();

        return NoContent();
    }
}