using BlogPessoal.Data;
using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Repositories.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogPessoal.Models;

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
    /*
    //ainda tem que fazer o Repository de usuario -------------------------------------------********************************


                        Lembrar de convergir para ASSÍNCroNO o SaveChangesAsync()

    [HttpPost]
    public ActionResult<UsuarioRequestDTO> Post(UsuarioRequestDTO usuRequestDto)
    {
        if (usuRequestDto is null)
            return BadRequest("Dados inválidos");

        var usuario = usuRequestDto.RequestToUsuario();

        //fazer o cadastro do usuario --------------------------********************************************************************_________________

        var usuarioCriado = _uof.UsuarioRepository.Create(usuario);
        _uof.Commit();//salva no banco

        var usuResponseDTO = usuarioCriado.ToUsuarioResponseDTO();

        return StatusCode(StatusCodes.Status201Created, usuResponseDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<UsuarioRequestDTO> Put(int id, UsuarioResponseDTO usuResponseDto)
    {
        if (id != usuResponseDto.UsuarioId)
            return BadRequest("Dados inválidos");

        var usuario = usuResponseDto.ResponseToUsuario();

        var usuarioAtualizado = _uof.UsuarioRepository.Update(usuario);
        _uof.Commit();

        var novoUsuarioResponseDto = usuarioAtualizado.ToUsuarioResponseDTO();

        return Ok(novoUsuarioResponseDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<UsuarioResponseDTO> Delete(int id)
    {
        var usuario = _uof.UsuarioRepository.Get(c => c.UsuarioId == id);

        if (usuario is null)
            return NotFound("Usuário não encontrado");

        var usuarioExcluido = _uof.UsuarioRepository.Delete(usuario);
        _uof.Commit();

        var usuResponseDto = usuarioExcluido.ToUsuarioResponseDTO();

        return Ok(usuResponseDto);
    }
    */
}
