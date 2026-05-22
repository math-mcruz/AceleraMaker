using BlogPessoal.DTOs.IA;
using BlogPessoal.Services.IA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BlogPessoal.Controllers;

[ApiController]
[Route("api/ia")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class IAController : ControllerBase
{
    private readonly IIAService _iaService;

    public IAController(IIAService iaService)
    {
        _iaService = iaService;
    }
    /// <summary>
    /// Resumir postagem.
    /// </summary>
    /// <remarks>
    /// Requer autenticação para usar a funcionalidade.
    /// 
    /// Exemplo de requisição:
    ///
    ///     POST /api/ia/resumir
    ///     {
    ///        "PostagemId": 1
    ///     }
    /// </remarks>
    /// <returns>Lista contendo todas as postagens e seus respectivos temas e autores.</returns>
    /// <response code="200">Resumo da postagem foi retornado com sucesso!</response>
    /// <response code="400">Postagem não existe.</response>
    [HttpPost("resumir")]
    [Authorize(Policy = "RequerUsuario")]
    [EnableRateLimiting("sliding")]
    public async Task<ActionResult<ResultadoIADTO>> Resumir([FromBody] ResumoIARequestDTO request)
    {
        if (string.IsNullOrWhiteSpace(request.Conteudo))
            return BadRequest(new { Mensagem = "Texto para resumo não pode estar vazio." });

        var resultado = await _iaService.GerarResumoAsync(request.Conteudo);
        return Ok(resultado);
    }
}