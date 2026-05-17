using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models;
using BlogPessoal.Repositories.UnitsOfWork;
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
    public ActionResult<IEnumerable<PostagemResponseDTO>> Get()
    {
        var postagem = _uof.PostagemRepository.GetAll();
        if (postagem is null)
            return NotFound("Não existem temas criados");

        var postResponseDTO = postagem.ToPostagemDTOList();

        return Ok(postResponseDTO);
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<Postagem>> GetAutorTema()
    {
        //resolver
    }

    [HttpPost]
    public ActionResult<PostagemRequestDTO> Post(PostagemRequestDTO postRequestDto)
    {
        if (postRequestDto is null)
            return BadRequest("Dados inválidos");

        var post = postRequestDto.RequestToPost();

        var postCriado = _uof.PostagemRepository.Create(post);
        _uof.Commit();

        var postResponseDto = postCriado.ToPostResponseDTO();

        return StatusCode(StatusCodes.Status201Created, postResponseDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<PostagemRequestDTO> Put(int id, PostagemResponseDTO postResponseDto)
    {
        if (id != postResponseDto.PostagemId)
            return BadRequest("Dados inválidos");

        var post = postResponseDto.ResponseToPost();

        var postAtualizado = _uof.PostagemRepository.Update(post);
        _uof.Commit();

        var novoPostResponseDto = postAtualizado.ToPostResponseDTO();

        return Ok(novoPostResponseDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var post = _uof.PostagemRepository.Get(c => c.PostagemId == id);
        if (post is null)
            return NotFound("Postagem não encontrado");


        var postExcluido = _uof.PostagemRepository.Delete(post);
        _uof.Commit();

        var postResponseDto = postExcluido.ToPostResponseDTO();

        return Ok(postResponseDto);
    }
}