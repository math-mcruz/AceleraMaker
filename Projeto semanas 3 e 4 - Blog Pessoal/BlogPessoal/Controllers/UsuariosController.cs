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
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Erro",
                Message = "Falha ao cadastrar."
            });
        }

        return Ok(new Response{ Status = "Sucesso", Message = "Usuário cadastrado com sucesso!"});
    }

    [HttpPost("login")]
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
                //new Claim(ClaimTypes.Name, user.UserName!), decidir se vai por o nome também ------------------------------*************
                new Claim(ClaimTypes.Email, user.Email!),
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
        return Unauthorized();
    }
    /*
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> Atualizar(int id, [FromBody] UsuarioRequestDTO usuarioAtualizado)
    {
        
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        
    }*/
}
