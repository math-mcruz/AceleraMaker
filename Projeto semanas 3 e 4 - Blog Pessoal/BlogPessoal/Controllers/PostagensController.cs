using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Middlewares.Extensions;
using BlogPessoal.Models.Pagination;
using BlogPessoal.Services.Postagens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BlogPessoal.Controllers;

[Route("api/postagens")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class PostagensController : ControllerBase
{
    private readonly IPostagemService _postagemService;

    public PostagensController(IPostagemService postagemService)
    {
        _postagemService = postagemService;
    }

    /// <summary>
    /// Listar todas as postagens.
    /// </summary>
    /// <remarks>
    /// Público, não requer autenticação.
    /// </remarks>
    /// <returns>Lista contendo todas as postagens e seus respectivos temas e autores.</returns>
    /// <response code="200">Lista de postagens foi retornada com sucesso.</response>
    /// <response code="404">Não existem postagens criados.</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> Get()
    {
        var resultado = await _postagemService.GetAllAsync();
        return Ok(resultado);
    }

    /// <summary>
    /// Filtrar postagens por autor e/ou tema.
    /// </summary>
    /// <remarks>
    /// Filtros são opcionais e podem ser combinados ou simples.
    /// 
    /// Exemplos de requisição:
    ///
    ///     Apenas por Autor:
    ///     GET /api/postagens/filtro?autor=1
    ///     
    ///     Apenas por Tema:
    ///     GET /api/postagens/filtro?tema=2
    ///     
    ///     Combinando os dois:
    ///     GET /api/postagens/filtro?autor=1&amp;tema=2
    /// </remarks>
    /// <param name="AutorId">Id do autor da postagem (Opcional).</param>
    /// <param name="TemaId">Id do tema vinculado à postagem (Opcional).</param>
    /// <returns>Lista contendo todas as postagens correspondentes aos critérios de busca.</returns>
    /// <response code="200">Lista de postagens foi retornada com sucesso.</response>
    /// <response code="404">Não existem postagens criados.</response>
    [HttpGet("filtro")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> GetFiltro([FromQuery] PostagensFiltroAutorTema postFiltro)
    {
        var resultado = await _postagemService.GetPorFiltroAsync(postFiltro);

        return Ok(resultado);
    }

    /// <summary>
    /// Criar uma nova postagem.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/postagens
    ///     {
    ///        "titulo": "Aprendendo a construir API",
    ///        "texto": "Durante as aulas do professor Macoratti foi...",
    ///        "temaId": 1
    ///     }
    ///
    /// </remarks>
    /// <param name="postagem">Título, texto e o ID do tema da postagem.</param>
    /// <returns>Retorna os dados da postagem criada.</returns>
    /// <response code="201">Postagem criada com sucesso!</response>
    /// <response code="400">Falha, possíveis erros: dados inválidos, formatação, falta de propriedades obrigatórias.</response>
    /// <response code="401">Não autorizado.</response>
    [HttpPost]
    [Authorize(Policy = "RequerUsuario")]
    [EnableRateLimiting("sliding")]
    public async Task<ActionResult<PostagemResponseDTO>> Post(PostagemRequestDTO postRequestDto)
    {
        int usuarioLogadoId = User.ObterIdUsuarioLogado();
        var resultado = await _postagemService.CreateAsync(postRequestDto, usuarioLogadoId);

        return StatusCode(StatusCodes.Status201Created, resultado);
    }

    /// <summary>
    /// Atualizar os dados de uma postagem.
    /// </summary>
    /// <remarks>
    /// **Observação:** apenas o autor original da postagem ou administrador pode atualizar
    /// 
    /// Exemplo de requisição:
    ///
    ///     PUT /api/postagens/5
    ///     {
    ///        "titulo": "O que aprendi construindo uma API",
    ///        "texto": "Muitas horas de dedicação, erros, validações...",
    ///        "temaId": 2
    ///     }
    ///
    /// </remarks>
    /// <param name="id">ID da postagem que será atualizada.</param>
    /// <param name="postagem">Novos dados que deseja atualizar.</param>
    /// <returns>Postagem com os dados atualizados.</returns>
    /// <response code="200">Postagem atualizada com sucesso!</response>
    /// <response code="400">Falha, possíveis erros: Id inválido e/ou dados inválidos, formatação, falta de propriedades obrigatórias.</response>
    /// <response code="401">Token JWT expirou ou inválido.</response>
    /// <response code="403">Sem permissão para alterar esta postagem.</response>
    /// <response code="404">Postagem não existe.</response>
    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequerUsuario")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PostagemResponseDTO>> Put(int id, PostagemUpdateDTO postUpdateDto)
    {
        int usuarioLogadoId = User.ObterIdUsuarioLogado();
        bool ehAdmin = User.IsInRole("Admin");

        var resultado = await _postagemService.UpdateAsync(id, postUpdateDto, usuarioLogadoId, ehAdmin);

        return Ok(resultado);
    }

    /// <summary>
    /// Excluir uma postagem.
    /// </summary>
    /// <remarks>
    /// **Observação:** apenas o autor original da postagem ou administrador pode excluir.
    /// </remarks>
    /// <param name="id">ID da postagem que será excluída.</param>
    /// <returns>Status de confirmação sem conteúdo.</returns>
    /// <response code="204">Postagem excluída com sucesso!</response>
    /// <response code="401">Token JWT expirou ou inválido.</response>
    /// <response code="403">Sem permissão para excluir a postagem.</response>
    /// <response code="404">Postagem não existe.</response>
    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PostagemResponseDTO>> Delete(int id)
    {
        int usuarioLogadoId = User.ObterIdUsuarioLogado();
        bool ehAdmin = User.IsInRole("Admin");

        await _postagemService.DeleteAsync(id, usuarioLogadoId, ehAdmin);

        return NoContent();
    }
}