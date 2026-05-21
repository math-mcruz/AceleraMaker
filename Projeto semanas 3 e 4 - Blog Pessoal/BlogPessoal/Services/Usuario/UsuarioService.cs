using BlogPessoal.DTOs.Status;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;
using BlogPessoal.Services.Token;
using BlogPessoal.Services.Usuario;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogPessoal.Services;

public class UsuarioService : IUsuarioService
{
    private readonly UserManager<BlogPessoal.Models.Usuario> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public UsuarioService(
        UserManager<BlogPessoal.Models.Usuario> userManager,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<Response> CadastrarAsync(UsuarioRequestDTO userCadastro)
    {
        var userExists = await _userManager.FindByEmailAsync(userCadastro.Email!);

        if (userExists != null)
            throw new InvalidOperationException("Usuário já existe.");

        var user = new BlogPessoal.Models.Usuario
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
            throw new ArgumentException($"Falha ao cadastrar: {erroCompleto}");
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Usuario");
        if (!roleResult.Succeeded)
            throw new Exception("Usuário criado, mas falhou ao vincular o perfil de 'Usuario'.");

        return new Response { Status = "Sucesso", Message = "Usuário cadastrado com sucesso!" };
    }

    public async Task<(string Token, DateTime Expiration)> LoginAsync(UsuarioLogin userLogin)
    {
        var user = await _userManager.FindByEmailAsync(userLogin.Email!);

        if (user is null || !await _userManager.CheckPasswordAsync(user, userLogin.Senha))
            throw new UnauthorizedAccessException("Email ou senha inválidos.");

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

        await _userManager.UpdateAsync(user);

        return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }

    public async Task AtualizarPerfilAsync(int id, UsuarioUpdateDTO userUpdateDto, int usuarioLogadoId, bool ehAdmin)
    {
        if (id != usuarioLogadoId && !ehAdmin)
            throw new UnauthorizedAccessException("Sem permissão para atualizar o perfil de outro usuário.");

        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        user.UserName = userUpdateDto.Nome;

        var resultado = await _userManager.UpdateAsync(user);

        if (!resultado.Succeeded)
        {
            var erros = string.Join(" | ", resultado.Errors.Select(e => e.Description));
            throw new ArgumentException($"Erro ao atualizar: {erros}");
        }
    }

    public async Task ExcluirContaAsync(int id, int usuarioLogadoId, bool ehAdmin)
    {
        if (id != usuarioLogadoId && !ehAdmin)
            throw new UnauthorizedAccessException("Sem permissão para excluir o perfil de outro usuário.");

        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        var resultado = await _userManager.DeleteAsync(user);

        if (!resultado.Succeeded)
            throw new Exception("Falha interna ao tentar excluir o usuário.");
    }
}