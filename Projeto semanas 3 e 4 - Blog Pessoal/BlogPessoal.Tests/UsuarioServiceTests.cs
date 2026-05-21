using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;
using BlogPessoal.Services;
using BlogPessoal.Services.Token;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.Extensions.Configuration;

namespace BlogPessoal.Tests;

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
}
