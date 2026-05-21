using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;
using BlogPessoal.Services;
using BlogPessoal.Services.Token;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.Extensions.Configuration;

namespace BlogPessoal.Tests.UsuarioTests;

public class UsuarioServiceTests
{
    private readonly Mock<UserManager<Usuario>> _mockUserManager;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly UsuarioService _usuarioService;

    public UsuarioServiceTests()
    {
        var store = new Mock<IUserStore<Usuario>>();
        _mockUserManager = new Mock<UserManager<Usuario>>(store.Object, null, null, null, null, null, null, null, null);
        _mockTokenService = new Mock<ITokenService>();
        _mockConfiguration = new Mock<IConfiguration>();

        _usuarioService = new UsuarioService(
            _mockUserManager.Object,
            _mockTokenService.Object,
            _mockConfiguration.Object
        );
    }

    [Fact]
    public async Task CadastrarAsync_Return_InvalidOperationException()
    {
        // ARRANGE
        var requestDto = new UsuarioRequestDTO
        {
            Email = "teste@email.com",
            Username = "Teste",
            Senha = "Senha@123"
        };

        _mockUserManager.Setup(u => u.FindByEmailAsync(requestDto.Email))
                        .ReturnsAsync(new Usuario { Email = requestDto.Email });

        // ACT
        Func<Task> acao = async () => await _usuarioService.CadastrarAsync(requestDto);

        // ASSERT
        await acao.Should().ThrowAsync<InvalidOperationException>()
                  .WithMessage("Usuário já existe.");
    }

    [Fact]
    public async Task LoginAsync_Return_TokenAndExpiration()
    {
        // ARRANGE
        var usuarioLogin = new UsuarioLogin
        {
            Email = "usuario@email.com",
            Senha = "SenhaValida@123"
        };

        var usuarioExistente = new Usuario
        {
            Id = 1,
            Email = "usuario@email.com",
            UserName = "usuario_teste"
        };

        var tokenGerado = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
            issuer: "BlogPessoalIssuer",
            audience: "BlogPessoalAudience",
            claims: new System.Collections.Generic.List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, usuarioExistente.UserName),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, usuarioExistente.Email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, usuarioExistente.Id.ToString())
            },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: null
        );

        _mockUserManager.Setup(u => u.FindByEmailAsync(usuarioLogin.Email))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.CheckPasswordAsync(usuarioExistente, usuarioLogin.Senha))
                        .ReturnsAsync(true);

        _mockUserManager.Setup(u => u.GetRolesAsync(usuarioExistente))
                        .ReturnsAsync(new System.Collections.Generic.List<string> { "Usuario" });

        _mockTokenService.Setup(t => t.GenerateAccessToken(
            It.IsAny<System.Collections.Generic.List<System.Security.Claims.Claim>>(),
            _mockConfiguration.Object))
            .Returns(tokenGerado);

        _mockUserManager.Setup(u => u.UpdateAsync(It.IsAny<Usuario>()))
                        .ReturnsAsync(IdentityResult.Success);

        // ACT
        var resultado = await _usuarioService.LoginAsync(usuarioLogin);

        // ASSERT
        resultado.Token.Should().NotBeNullOrEmpty();
        resultado.Expiration.Should().BeAfter(DateTime.UtcNow);
        resultado.Expiration.Should().BeBefore(DateTime.UtcNow.AddHours(2));

        _mockUserManager.Verify(u => u.FindByEmailAsync(usuarioLogin.Email), Times.Once);
        _mockUserManager.Verify(u => u.CheckPasswordAsync(usuarioExistente, usuarioLogin.Senha), Times.Once);
        _mockUserManager.Verify(u => u.GetRolesAsync(usuarioExistente), Times.Once);
        _mockUserManager.Verify(u => u.UpdateAsync(usuarioExistente), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_InvalidEmail_UnauthorizedAccessException()
    {
        // ARRANGE
        var usuarioLogin = new UsuarioLogin
        {
            Email = "emailinvalido@email.com",
            Senha = "SenhaValida@123"
        };

        _mockUserManager.Setup(u => u.FindByEmailAsync(usuarioLogin.Email))
                        .ReturnsAsync((Usuario?)null);

        // ACT
        Func<Task> acao = async () => await _usuarioService.LoginAsync(usuarioLogin);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Email ou senha inválidos.");

        _mockUserManager.Verify(u => u.FindByEmailAsync(usuarioLogin.Email), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_UnauthorizedAccessException()
    {
        // ARRANGE
        var usuarioLogin = new UsuarioLogin
        {
            Email = "usuario@email.com",
            Senha = "SenhaInvalida@123"
        };

        var usuarioExistente = new Usuario
        {
            Id = 1,
            Email = "usuario@email.com",
            UserName = "usuario_teste"
        };

        _mockUserManager.Setup(u => u.FindByEmailAsync(usuarioLogin.Email))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.CheckPasswordAsync(usuarioExistente, usuarioLogin.Senha))
                        .ReturnsAsync(false);

        // ACT
        Func<Task> acao = async () => await _usuarioService.LoginAsync(usuarioLogin);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Email ou senha inválidos.");

        _mockUserManager.Verify(u => u.FindByEmailAsync(usuarioLogin.Email), Times.Once);
        _mockUserManager.Verify(u => u.CheckPasswordAsync(usuarioExistente, usuarioLogin.Senha), Times.Once);
    }
}