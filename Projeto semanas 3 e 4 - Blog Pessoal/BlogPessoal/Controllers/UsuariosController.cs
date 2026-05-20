using BlogPessoal.Data;
using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Status;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace BlogPessoal.Controllers;

[Route("api/usuarios")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;

    public UsuariosController(ITokenService tokenService, UserManager<Usuario> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost("cadastrar")]
    [AllowAnonymous]
    public async Task<ActionResult> Cadastrar([FromBody] UsuarioRequestDTO userCadastro)
    {
        var userExists = await _userManager.FindByEmailAsync(userCadastro.Email!);

        if (userExists != null) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Erro",
                Message = "Usuário já existe."
            });
        }
        Usuario user = new()
        {
            Email = userCadastro.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userCadastro.Username
        };

        var result = await _userManager.CreateAsync(user, userCadastro.Senha);

        if (!result.Succeeded)
        {
            var mensagensDeErro = result.Errors.Select(e => e.Description);
            var erroCompleto = string.Join(" | ", mensagensDeErro);

            // 2. Retornamos 400 BadRequest com o detalhe do erro
            return BadRequest(new Response
            {
                Status = "Erro",
                Message = $"Falha ao cadastrar: {erroCompleto}"
            });
            //return StatusCode(StatusCodes.Status500InternalServerError, new Response
            //{
            //    Status = "Erro",
            //    Message = "Falha ao cadastrar."

            //});
        }
        var roleResult = await _userManager.AddToRoleAsync(user, "Usuario");
        if (!roleResult.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Aviso",
                Message = "Usuário criado, mas falhou ao vincular o perfil de 'Usuario'."
            });
        }

        return Ok(new Response{ Status = "Sucesso", Message = "Usuário cadastrado com sucesso!"});
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] UsuarioLogin userLogin)
    {
        //encontrar o usuário
        var user = await _userManager.FindByEmailAsync(userLogin.Email!);
        
        //verifica se existe o usuario e se a senha é a mesma
        if (user is not null && await _userManager.CheckPasswordAsync(user, userLogin.Senha))
        {
            //busca os perfis do usuário
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClains = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var userRole in userRoles)
            {
                //entender melhor como funciona ------------------------------------------------------------************
                authClains.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClains, _configuration);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }
        return Unauthorized("Email ou senha inválidos.");
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult> Atualizar(int id, UsuarioUpdateDTO userUpdateDto)
    {
        var claimValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userLogadoId))
            return Unauthorized("Token inválido.");

        bool ehAdmin = User.IsInRole("Admin");

        if (id != userLogadoId && !ehAdmin)
            return Forbid("Sem permissão para atualizar o perfil de outro usuário.");

        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
            return NotFound("Usuário não encontrado.");

        user.UserName = userUpdateDto.Nome;

        var resultado = await _userManager.UpdateAsync(user);

        if (!resultado.Succeeded)
            return BadRequest(resultado.Errors);


        return Ok("Perfil atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> Excluir(int id)
    {
        var claimValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userLogadoId))
            return Unauthorized("Token inválido.");

        bool ehAdmin = User.IsInRole("Admin");

        if (id != userLogadoId && !ehAdmin)
            return Forbid("Sem permissão para excluir o perfil de outro usuário.");

        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
            return NotFound("Usuário não encontrado.");

        var resultado = await _userManager.DeleteAsync(user);

        if (!resultado.Succeeded)
            return NoContent();


        return Ok("Perfil excluído com sucesso!");
    }
}
