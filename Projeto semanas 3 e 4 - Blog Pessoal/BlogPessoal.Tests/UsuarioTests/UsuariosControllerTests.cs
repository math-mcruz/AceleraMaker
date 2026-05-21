using BlogPessoal.Controllers;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;
using BlogPessoal.Services.Usuario;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogPessoal.Tests.UsuarioTests;

public class UsuariosControllerTests
{
    private readonly Mock<IUsuarioService> _mockUsuarioService;
    private readonly UsuariosController _usuariosController;

    public UsuariosControllerTests()
    {
        _mockUsuarioService = new Mock<IUsuarioService>();
        _usuariosController = new UsuariosController(_mockUsuarioService.Object);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsOkWithToken()
    {
        // ARRANGE
        var usuarioLogin = new UsuarioLogin
        {
            Email = "usuario@email.com",
            Senha = "SenhaValida@123"
        };

        var tokenValido = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        var expiracao = DateTime.UtcNow.AddHours(1);

        _mockUsuarioService.Setup(u => u.LoginAsync(usuarioLogin))
                           .ReturnsAsync((tokenValido, expiracao));

        // ACT
        var resultado = await _usuariosController.Login(usuarioLogin);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<OkObjectResult>();

        var okResult = resultado as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);

        var responseValue = okResult?.Value;
        responseValue.Should().NotBeNull();

        var tokenProperty = responseValue?.GetType().GetProperty("Token");
        tokenProperty?.GetValue(responseValue)?.Should().Be(tokenValido);

        var expirationProperty = responseValue?.GetType().GetProperty("Expiration");
        expirationProperty?.GetValue(responseValue)?.Should().Be(expiracao);

        _mockUsuarioService.Verify(u => u.LoginAsync(usuarioLogin), Times.Once);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        // ARRANGE
        var usuarioLogin = new UsuarioLogin
        {
            Email = "emailinvalido@email.com",
            Senha = "SenhaInvalida@123"
        };

        _mockUsuarioService.Setup(u => u.LoginAsync(usuarioLogin))
                           .ThrowsAsync(new UnauthorizedAccessException("Email ou senha inválidos."));

        // ACT
        Func<Task> acao = async () => await _usuariosController.Login(usuarioLogin);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Email ou senha inválidos.");

        _mockUsuarioService.Verify(u => u.LoginAsync(usuarioLogin), Times.Once);
    }

    [Fact]
    public async Task Cadastrar_WithValidData_ReturnsOkWithSuccessMessage()
    {
        // ARRANGE
        var usuarioRequestDto = new UsuarioRequestDTO
        {
            Username = "NovoUsuario",
            Email = "novousuario@email.com",
            Senha = "SenhaValida@123"
        };

        var responseEsperada = new DTOs.Status.Response
        {
            Status = "Sucesso",
            Message = "Usuário cadastrado com sucesso!"
        };

        _mockUsuarioService.Setup(u => u.CadastrarAsync(usuarioRequestDto))
                           .ReturnsAsync(responseEsperada);

        // ACT
        var resultado = await _usuariosController.Cadastrar(usuarioRequestDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Should().BeOfType<OkObjectResult>();

        var okResult = resultado as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);
        okResult?.Value.Should().Be(responseEsperada);

        _mockUsuarioService.Verify(u => u.CadastrarAsync(usuarioRequestDto), Times.Once);
    }

    [Fact]
    public async Task Cadastrar_WithDuplicateEmail_ThrowsInvalidOperationException()
    {
        // ARRANGE
        var usuarioRequestDto = new UsuarioRequestDTO
        {
            Username = "UsuarioExistente",
            Email = "existente@email.com",
            Senha = "SenhaValida@123"
        };

        _mockUsuarioService.Setup(u => u.CadastrarAsync(usuarioRequestDto))
                           .ThrowsAsync(new InvalidOperationException("Usuário já existe."));

        // ACT
        Func<Task> acao = async () => await _usuariosController.Cadastrar(usuarioRequestDto);

        // ASSERT
        await acao.Should().ThrowAsync<InvalidOperationException>()
                  .WithMessage("Usuário já existe.");

        _mockUsuarioService.Verify(u => u.CadastrarAsync(usuarioRequestDto), Times.Once);
    }

    [Fact]
    public async Task Cadastrar_WithInvalidData_ThrowsArgumentException()
    {
        // ARRANGE
        var usuarioRequestDto = new UsuarioRequestDTO
        {
            Username = "Usuario",
            Email = "invalido@email.com",
            Senha = "Fraca123" // Senha que não atende aos requisitos
        };

        _mockUsuarioService.Setup(u => u.CadastrarAsync(usuarioRequestDto))
                           .ThrowsAsync(new ArgumentException("Falha ao cadastrar: senha inválida"));

        // ACT
        Func<Task> acao = async () => await _usuariosController.Cadastrar(usuarioRequestDto);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("*Falha ao cadastrar*");

        _mockUsuarioService.Verify(u => u.CadastrarAsync(usuarioRequestDto), Times.Once);
    }

    [Fact]
    public async Task Login_WithNullCredentials_ThrowsException()
    {
        // ARRANGE
        UsuarioLogin usuarioLogin = null!;

        _mockUsuarioService.Setup(u => u.LoginAsync(It.IsAny<UsuarioLogin>()))
                           .ThrowsAsync(new ArgumentNullException(nameof(usuarioLogin)));

        // ACT
        Func<Task> acao = async () => await _usuariosController.Login(usuarioLogin!);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentNullException>();

        _mockUsuarioService.Verify(u => u.LoginAsync(It.IsAny<UsuarioLogin>()), Times.Once);
    }
}
