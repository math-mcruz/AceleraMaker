using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services.Postagens;
using FluentAssertions;
using Moq;

namespace BlogPessoal.Tests.PostagemTests;

public class PostagemServiceTests
{
    private readonly Mock<IUnitOfWork> _uofMock;
    private readonly PostagemService _postagemService;

    public PostagemServiceTests()
    {
        _uofMock = new Mock<IUnitOfWork>();
        _postagemService = new PostagemService(_uofMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Return_PostResponseDTO()
    {
        // ARRANGE
        var postRequestDto = new PostagemRequestDTO
        {
            Titulo = "Testando postagem",
            Texto = "Texto da postagem",
            TemaId = 1
        };

        var usuarioLogadoId = 1;

        var temaMock = new Tema { TemaId = 1, Nome = "C#" };

        var postagemCriada = new Postagem
        {
            PostagemId = 1,
            Titulo = "Testando postagem",
            Texto = "Texto da postagem",
            Data = DateTime.Now,
            UsuarioId = usuarioLogadoId,
            TemaId = 1,
            Usuario = new Usuario { Id = usuarioLogadoId, UserName = "user1" },
            Tema = temaMock
        };

        _uofMock.Setup(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()))
                .ReturnsAsync(temaMock);

        _uofMock.Setup(u => u.PostagemRepository.Create(It.IsAny<Postagem>()))
                .Returns(postagemCriada);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Postagem, bool>>>()))
                .ReturnsAsync(postagemCriada);

        // ACT
        var resultado = await _postagemService.CreateAsync(postRequestDto, usuarioLogadoId);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Titulo.Should().Be("Testando postagem");
        resultado.Texto.Should().Be("Texto da postagem");
        resultado.TemaId.Should().Be(1);
        resultado.UsuarioId.Should().Be(usuarioLogadoId);
        resultado.NomeAutor.Should().Be("user1");

        _uofMock.Verify(u => u.PostagemRepository.Create(It.IsAny<Postagem>()), Times.Once);
        _uofMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_Return_ThrowsArgumentException()
    {
        // ARRANGE
        PostagemRequestDTO? postRequestDto = null;
        var usuarioLogadoId = 1;

        // ACT
        Func<Task> acao = async () => await _postagemService.CreateAsync(postRequestDto, usuarioLogadoId);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("Dados inválidos.");
    }

    [Fact]
    public async Task CreateAsync_Return_KeyNotFoundException()
    {
        // ARRANGE
        var postRequestDto = new PostagemRequestDTO
        {
            Titulo = "Post com Tema Inválido",
            Texto = "Conteúdo do post",
            TemaId = 999 
        };

        var usuarioLogadoId = 1;

        // Mock para simular que o tema não existe
        _uofMock.Setup(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()))
                .ReturnsAsync((Tema?)null);

        // ACT
        Func<Task> acao = async () => await _postagemService.CreateAsync(postRequestDto, usuarioLogadoId);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Tema não encontrado.");
    }
}
