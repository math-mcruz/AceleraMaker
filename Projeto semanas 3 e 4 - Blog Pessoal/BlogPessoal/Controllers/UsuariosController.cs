using BlogPessoal.DTOs.Status;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Middlewares.Extensions;
using BlogPessoal.Models;
using BlogPessoal.Services.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BlogPessoal.Controllers;

[Route("api/usuarios")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Criar nova conta de usuário no Blog.
    /// </summary>
    /// <remarks>
    /// Novos usuários recebem o perfil de acesso: **Usuario**.
    /// 
    /// **Senha deve conter:**
    ///  * **Mínimo de 6 caracteres**
    ///  * **Pelo menos uma letra maiúscula e uma minúscula**
    ///  * **Pelo menos um número e um caractere especial (!, @, #, $, ...)**
    /// 
    /// Exemplo de requisição:
    ///
    ///     POST /api/usuarios/cadastrar
    ///     {
    ///        "nome": "Luis Guerreiro",
    ///        "email": "luisg@email.com",
    ///        "senha": "SenhaSuperSegura@123"
    ///     }
    ///
    /// </remarks>
    /// <param name="userCadastro">Dados essenciais para o cadastro.</param>
    /// <returns>Mensagem de sucesso confirmando a criação da conta.</returns>
    /// <response code="200">Usuário adastrado com sucesso!</response>
    /// <response code="400">Falha, possíveis erros: dados inválidos, formatação, falta de propriedades obrigatórias, senha inválida.</response>
    /// <response code="500">Falhou ao vincular o perfil.</response>
    [HttpPost("cadastrar")]
    [AllowAnonymous]
    [EnableRateLimiting("sliding")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Cadastrar([FromBody] UsuarioRequestDTO userCadastro)
    {
        var response = await _usuarioService.CadastrarAsync(userCadastro);
        return Ok(response);
    }

    /// <summary>
    /// Login com validação de email e senha.
    /// </summary>
    /// <remarks>
    /// Geração do tokens JWT para autenticação, copie o Token retornado e cole no botão **Authorize** no topo da página para acessar as rotas protegidas.
    /// 
    /// Exemplo de requisição:
    ///     
    ///     POST /api/usuarios/login
    ///     {
    ///        "email": "luisg@email.com",
    ///        "senha": "SenhaSuperSegura@123"
    ///     }
    ///     
    /// </remarks>
    /// <param name="usuarioLogin">Credenciais de acesso.</param>
    /// <returns>Retorna o Token JWT que autoriza o acesso de rotas protegidas.</returns>
    /// <response code="200">Login realizado com sucesso! Token JWT gerado.</response>
    /// <response code="400">Falha, possíveis erros: formatação, e-mail ou senha inválidos.</response>
    /// <response code="401">E-mail não encontrado ou senha incorreta.</response>
    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("sliding")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Login([FromBody] UsuarioLogin userLogin)
    {
        var (tokenString, expiracao) = await _usuarioService.LoginAsync(userLogin);

        return Ok(new
        {
            Token = tokenString,
            Expiration = expiracao
        });
    }

    /// <summary>
    /// Atualizar informações de perfil.
    /// </summary>
    /// <remarks>
    /// **Observação:** apenas o usuário do perfil ou administrador pode atualizar a conta.
    /// 
    /// Exemplo de requisição:
    ///
    ///     PUT /api/usuarios/7
    ///     {
    ///        "nome": "Aline",
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Id  do usuário que será atualizado.</param>
    /// <param name="usuarioAtualizacao">Novas informações do perfil.</param>
    /// <returns>Dados do usuário atualizado.</returns>
    /// <response code="200">Perfil foi atualizado com sucesso!</response>
    /// <response code="400">Falha, possíveis erros: Id inválido e/ou dados inválidos, formatação, falta de propriedades obrigatórias.</response>
    /// <response code="401">Token JWT expirou ou inválido.</response>
    /// <response code="403">Sem permissão para alterar a conta.</response>
    /// <response code="404">Usuário não existe.</response>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Atualizar(int id, UsuarioUpdateDTO userUpdateDto)
    {
        int usuarioLogadoId = User.ObterIdUsuarioLogado();
        bool ehAdmin = User.IsInRole("Admin");

        await _usuarioService.AtualizarPerfilAsync(id, userUpdateDto, usuarioLogadoId, ehAdmin);

        return Ok("Perfil atualizado com sucesso!");
    }

    /// <summary>
    /// Excluir conta de usuário.
    /// </summary>
    /// <remarks>
    /// **Observação:** apenas o usuário do perfil ou administrador pode excluir a conta.
    /// </remarks>
    /// <param name="id">ID da conta a ser excluída.</param>
    /// <returns>Status de confirmação sem conteúdo.</returns>
    /// <response code="204">Conta excluída com sucesso!</response>
    /// <response code="401">Token JWT expirou ou inválido.</response>
    /// <response code="403">Sem permissão para para excluir a conta.</response>
    /// <response code="404">Conta não existe.</response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Excluir(int id)
    {
        int usuarioLogadoId = User.ObterIdUsuarioLogado();
        bool ehAdmin = User.IsInRole("Admin");

        await _usuarioService.ExcluirContaAsync(id, usuarioLogadoId, ehAdmin);

        return NoContent();
    }
}