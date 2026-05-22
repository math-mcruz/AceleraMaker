using BlogPessoal.DTOs.IA;
using BlogPessoal.Middlewares.Extensions;
using BlogPessoal.Services.IA;
using BlogPessoal.Services.Postagens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BlogPessoal.Controllers;

[ApiController]
[Route("api/ia")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class IAController : ControllerBase
{
    private readonly IPostagemService _postService;

    public IAController(IPostagemService postService)
    {
        _postService = postService;
    }
    /// <summary>
    /// Resumir postagem.
    /// </summary>
    /// <remarks>
    /// Requer autenticação para usar a funcionalidade.
    /// </remarks>
    /// <returns>Postagem resumida pelo Gemini Flash 3.5.</returns>
    /// <response code="200">Resumo da postagem foi retornado com sucesso!</response>
    /// <response code="400">Postagem não existe.</response>
    /// <response code="401">Não autorizado.</response>
    [HttpPost("resumir/{id}")]
    [Authorize(Policy = "RequerUsuario")]
    [EnableRateLimiting("sliding")]
    public async Task<ActionResult<ResultadoIADTO>> Resumir(int id)
    {
        var resultado = await _postService.GerarResumoIAAsync(id);

        return Ok(resultado);
    }
}