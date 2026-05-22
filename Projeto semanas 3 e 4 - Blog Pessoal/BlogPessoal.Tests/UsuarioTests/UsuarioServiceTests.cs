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

    [Fact]
    public async Task CadastrarAsync_Sucesso_DeveRetornarResponse()
    {
        // ARRANGE
        var requestDto = new UsuarioRequestDTO
        {
            Email = "novousuario@email.com",
            Username = "novousuario",
            Senha = "SenhaValida@123"
        };

        _mockUserManager.Setup(u => u.FindByEmailAsync(requestDto.Email))
                        .ReturnsAsync((Usuario?)null);

        _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<Usuario>(), requestDto.Senha))
                        .ReturnsAsync(IdentityResult.Success);

        _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<Usuario>(), "Usuario"))
                        .ReturnsAsync(IdentityResult.Success);

        // ACT
        var resultado = await _usuarioService.CadastrarAsync(requestDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Status.Should().Be("Sucesso");
        resultado.Message.Should().Be("Usuário cadastrado com sucesso!");

        _mockUserManager.Verify(u => u.FindByEmailAsync(requestDto.Email), Times.Once);
        _mockUserManager.Verify(u => u.CreateAsync(It.IsAny<Usuario>(), requestDto.Senha), Times.Once);
        _mockUserManager.Verify(u => u.AddToRoleAsync(It.IsAny<Usuario>(), "Usuario"), Times.Once);
    }

    [Fact]
    public async Task CadastrarAsync_ErroCreateAsync_DeveRetornarArgumentException()
    {
        // ARRANGE
        var requestDto = new UsuarioRequestDTO
        {
            Email = "usuario@email.com",
            Username = "usuario",
            Senha = "Senha@123"
        };

        var erroIdentity = new IdentityError
        {
            Code = "PasswordTooShort",
            Description = "A senha é muito curta."
        };

        _mockUserManager.Setup(u => u.FindByEmailAsync(requestDto.Email))
                        .ReturnsAsync((Usuario?)null);

        _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<Usuario>(), requestDto.Senha))
                        .ReturnsAsync(IdentityResult.Failed(erroIdentity));

        // ACT
        Func<Task> acao = async () => await _usuarioService.CadastrarAsync(requestDto);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("*Falha ao cadastrar*");

        _mockUserManager.Verify(u => u.FindByEmailAsync(requestDto.Email), Times.Once);
        _mockUserManager.Verify(u => u.CreateAsync(It.IsAny<Usuario>(), requestDto.Senha), Times.Once);
    }

    [Fact]
    public async Task CadastrarAsync_ErroAddToRoleAsync_DeveRetornarException()
    {
        // ARRANGE
        var requestDto = new UsuarioRequestDTO
        {
            Email = "usuario@email.com",
            Username = "usuario",
            Senha = "Senha@123"
        };

        var erroRole = new IdentityError
        {
            Code = "RoleNotFound",
            Description = "A função 'Usuario' não existe."
        };

        _mockUserManager.Setup(u => u.FindByEmailAsync(requestDto.Email))
                        .ReturnsAsync((Usuario?)null);

        _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<Usuario>(), requestDto.Senha))
                        .ReturnsAsync(IdentityResult.Success);

        _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<Usuario>(), "Usuario"))
                        .ReturnsAsync(IdentityResult.Failed(erroRole));

        // ACT
        Func<Task> acao = async () => await _usuarioService.CadastrarAsync(requestDto);

        // ASSERT
        await acao.Should().ThrowAsync<Exception>()
                  .WithMessage("Usuário criado, mas falhou ao vincular o perfil de 'Usuario'.");

        _mockUserManager.Verify(u => u.FindByEmailAsync(requestDto.Email), Times.Once);
        _mockUserManager.Verify(u => u.CreateAsync(It.IsAny<Usuario>(), requestDto.Senha), Times.Once);
        _mockUserManager.Verify(u => u.AddToRoleAsync(It.IsAny<Usuario>(), "Usuario"), Times.Once);
    }

    [Fact]
    public async Task AtualizarPerfilAsync_Sucesso_DeveAtualizarUsuario()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 1;
        var updateDto = new UsuarioUpdateDTO { Nome = "Novo Nome" };

        var usuarioExistente = new Usuario
        {
            Id = usuarioId,
            Email = "usuario@email.com",
            UserName = "usuario_antigo"
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.UpdateAsync(It.IsAny<Usuario>()))
                        .ReturnsAsync(IdentityResult.Success);

        // ACT
        Func<Task> acao = async () => await _usuarioService.AtualizarPerfilAsync(usuarioId, updateDto, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().NotThrowAsync();

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task AtualizarPerfilAsync_AcessoNegado_DeveRetornarUnauthorizedAccessException()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 2;
        var updateDto = new UsuarioUpdateDTO { Nome = "Novo Nome" };

        // ACT
        Func<Task> acao = async () => await _usuarioService.AtualizarPerfilAsync(usuarioId, updateDto, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Sem permissão para atualizar o perfil de outro usuário.");

        _mockUserManager.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task AtualizarPerfilAsync_AdminAtualizandoOutroUsuario_DeveAtualizarComSucesso()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 2;
        var updateDto = new UsuarioUpdateDTO { Nome = "Novo Nome" };

        var usuarioExistente = new Usuario
        {
            Id = usuarioId,
            Email = "usuario@email.com",
            UserName = "usuario_antigo"
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.UpdateAsync(It.IsAny<Usuario>()))
                        .ReturnsAsync(IdentityResult.Success);

        // ACT
        Func<Task> acao = async () => await _usuarioService.AtualizarPerfilAsync(usuarioId, updateDto, usuarioLogadoId, ehAdmin: true);

        // ASSERT
        await acao.Should().NotThrowAsync();

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task AtualizarPerfilAsync_UsuarioNaoEncontrado_DeveRetornarKeyNotFoundException()
    {
        // ARRANGE
        int usuarioId = 999;
        int usuarioLogadoId = 999;
        var updateDto = new UsuarioUpdateDTO { Nome = "Novo Nome" };

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync((Usuario?)null);

        // ACT
        Func<Task> acao = async () => await _usuarioService.AtualizarPerfilAsync(usuarioId, updateDto, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Usuário não encontrado.");

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.UpdateAsync(It.IsAny<Usuario>()), Times.Never);
    }

    [Fact]
    public async Task AtualizarPerfilAsync_ErroNaAtualizacao_DeveRetornarArgumentException()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 1;
        var updateDto = new UsuarioUpdateDTO { Nome = "Novo Nome" };

        var usuarioExistente = new Usuario
        {
            Id = usuarioId,
            Email = "usuario@email.com",
            UserName = "usuario_antigo"
        };

        var erroUpdate = new IdentityError
        {
            Code = "UpdateFailed",
            Description = "Falha ao atualizar usuário."
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.UpdateAsync(It.IsAny<Usuario>()))
                        .ReturnsAsync(IdentityResult.Failed(erroUpdate));

        // ACT
        Func<Task> acao = async () => await _usuarioService.AtualizarPerfilAsync(usuarioId, updateDto, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("*Erro ao atualizar*");

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task ExcluirContaAsync_Sucesso_DeveExcluirUsuario()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 1;

        var usuarioExistente = new Usuario
        {
            Id = usuarioId,
            Email = "usuario@email.com",
            UserName = "usuario_teste"
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.DeleteAsync(usuarioExistente))
                        .ReturnsAsync(IdentityResult.Success);

        // ACT
        Func<Task> acao = async () => await _usuarioService.ExcluirContaAsync(usuarioId, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().NotThrowAsync();

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.DeleteAsync(usuarioExistente), Times.Once);
    }

    [Fact]
    public async Task ExcluirContaAsync_AcessoNegado_DeveRetornarUnauthorizedAccessException()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 2;

        // ACT
        Func<Task> acao = async () => await _usuarioService.ExcluirContaAsync(usuarioId, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Sem permissão para excluir o perfil de outro usuário.");

        _mockUserManager.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ExcluirContaAsync_AdminExcluindoOutroUsuario_DeveExcluirComSucesso()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 2;

        var usuarioExistente = new Usuario
        {
            Id = usuarioId,
            Email = "usuario@email.com",
            UserName = "usuario_teste"
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.DeleteAsync(usuarioExistente))
                        .ReturnsAsync(IdentityResult.Success);

        // ACT
        Func<Task> acao = async () => await _usuarioService.ExcluirContaAsync(usuarioId, usuarioLogadoId, ehAdmin: true);

        // ASSERT
        await acao.Should().NotThrowAsync();

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.DeleteAsync(usuarioExistente), Times.Once);
    }

    [Fact]
    public async Task ExcluirContaAsync_UsuarioNaoEncontrado_DeveRetornarKeyNotFoundException()
    {
        // ARRANGE
        int usuarioId = 999;
        int usuarioLogadoId = 999;

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync((Usuario?)null);

        // ACT
        Func<Task> acao = async () => await _usuarioService.ExcluirContaAsync(usuarioId, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Usuário não encontrado.");

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.DeleteAsync(It.IsAny<Usuario>()), Times.Never);
    }

    [Fact]
    public async Task ExcluirContaAsync_ErroNaExclusao_DeveRetornarException()
    {
        // ARRANGE
        int usuarioId = 1;
        int usuarioLogadoId = 1;

        var usuarioExistente = new Usuario
        {
            Id = usuarioId,
            Email = "usuario@email.com",
            UserName = "usuario_teste"
        };

        var erroDelete = new IdentityError
        {
            Code = "DeleteFailed",
            Description = "Falha ao excluir usuário."
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(usuarioId.ToString()))
                        .ReturnsAsync(usuarioExistente);

        _mockUserManager.Setup(u => u.DeleteAsync(usuarioExistente))
                        .ReturnsAsync(IdentityResult.Failed(erroDelete));

        // ACT
        Func<Task> acao = async () => await _usuarioService.ExcluirContaAsync(usuarioId, usuarioLogadoId, ehAdmin: false);

        // ASSERT
        await acao.Should().ThrowAsync<Exception>()
                  .WithMessage("Falha interna ao tentar excluir o usuário.");

        _mockUserManager.Verify(u => u.FindByIdAsync(usuarioId.ToString()), Times.Once);
        _mockUserManager.Verify(u => u.DeleteAsync(usuarioExistente), Times.Once);
    }
}
