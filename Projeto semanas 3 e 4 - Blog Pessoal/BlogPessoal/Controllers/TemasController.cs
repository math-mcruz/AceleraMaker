using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Services.Tema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[Route("api/temas")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class TemasController : ControllerBase
{
    private readonly ITemaService _temaService;

    public TemasController(ITemaService temaService)
    {
        _temaService = temaService;
    }

    /// <summary>
    /// Listar todos os temas.
    /// </summary>
    /// <remarks>
    /// Público, não requer autenticação.
    /// </remarks>
    /// <returns>Lista contendo todos os temas e seus respectivos autores.</returns>
    /// <response code="200">Lista de postagens foi retornada com sucesso.</response>
    /// <response code="404">Não existem temas criados</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<TemaResponseDTO>>> Get()
    {
        var temasResponseDto = await _temaService.GetAllAsync();
        return Ok(temasResponseDto);
    }
    /// <summary>
    /// Criar novo tema de postagens.
    /// </summary>
    /// <remarks>
    /// **Observação:** apenas **ADMINISTRADOR** pode criar novos temas.
    /// 
    /// Exemplo de requisição:
    ///
    ///     POST /api/temas
    ///     {
    ///        "Nome": "API ASP .NET Core"
    ///     }
    ///
    /// </remarks>
    /// <param name="tema">Nome do novo tema.</param>
    /// <returns>Dados do tema criado.</returns>
    /// <response code="201">tema criado com sucesso!</response>
    /// <response code="400">Falha, possíveis erros: dado inválido ou formatação.</response>
    /// <response code="401">Token JWT expirou ou inválido.</response>
    /// <response code="403">Apenas o administrador pode criar um tema.</response>
    [HttpPost]
    [Authorize(Policy = "RequerAdmin")]
    public async Task<ActionResult<TemaResponseDTO>> Post(TemaRequestDTO temaRequestDto)
    {
        var temaResponseDto = await _temaService.CreateAsync(temaRequestDto);
        return StatusCode(StatusCodes.Status201Created, temaResponseDto);
    }

    /// <summary>
    /// Atualizar tema de postagens.
    /// </summary>
    /// <remarks>
    /// **Observação:** apenas **ADMINISTRADOR** pode atualizar temas.
    /// 
    /// Exemplo de requisição:
    ///
    ///     PUT /api/temas/2
    ///     {
    ///        "temaId": "2",
    ///        "nome": "Cobol"
    ///     }
    ///
    /// </remarks>
    /// <param name="temaId">Id do tema que deseja atualizar.</param>
    /// <param name="nome">Nome do novo tema.</param>
    /// <returns>Dados do tema atualizado.</returns>
    /// <response code="201">tema atualizado com sucesso!</response>
    /// <response code="400">Falha, possíveis erros: dados inválidos ou formatação.</response>
    /// <response code="401">Token JWT expirou ou inválido.</response>
    /// <response code="403">Apenas o administrador pode atualizar um tema.</response>
    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequerAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TemaResponseDTO>> Put(int id, TemaUpdateDTO temaUpdateDto)
    {
        var novoTemaResponseDto = await _temaService.UpdateAsync(id, temaUpdateDto);
        return Ok(novoTemaResponseDto);
    }

    /// <summary>
    /// Excluir tema existente.
    /// </summary>
    /// <remarks>
    /// **Observação:** apenas **ADMINISTRADOR** pode excluir temas existentes.
    /// </remarks>
    /// <param name="id">ID do tema a ser excluído.</param>
    /// <returns>Status de confirmação sem conteúdo.</returns>
    /// <response code="204">Tema excluído com sucesso!</response>
    /// <response code="401">Token JWT expirou ou inválido.</response>
    /// <response code="403">Apenas o administrador pode excluir um tema.</response>
    /// <response code="404">Tema não existe.</response>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "RequerAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TemaResponseDTO>> Delete(int id)
    {
        await _temaService.DeleteAsync(id);
        return NoContent();
    }
}
