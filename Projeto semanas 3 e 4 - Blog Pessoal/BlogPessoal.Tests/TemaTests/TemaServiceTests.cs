using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services.Temas;
using FluentAssertions;
using Moq;

namespace BlogPessoal.Tests.TemaTests;

public class TemaServiceTests
{
    private readonly Mock<IUnitOfWork> _uofMock;
    private readonly TemaService _temaService;

    public TemaServiceTests()
    {
        _uofMock = new Mock<IUnitOfWork>();
        _temaService = new TemaService(_uofMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_Return_KeyNotFoundException()
    {
        //ARRANGE
        var listaVazia = new List<Tema>();

        _uofMock.Setup(u => u.TemaRepository.GetAllAsync())
                .ReturnsAsync(listaVazia);

        //ACT
        Func<Task> acao = async () => await _temaService.GetAllAsync();

        //ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Não existem temas criados");
    }

    [Fact]
    public async Task GetAllAsync_Return_TemaDTOList()
    {
        //ARRANGE
        var listaTemasNoBanco = new List<Tema>
        {
            new Tema { TemaId = 1, Nome = "API ASP .NET Core" },
            new Tema { TemaId = 2, Nome = "Cobol" }
        };

        _uofMock.Setup(u => u.TemaRepository.GetAllAsync())
                .ReturnsAsync(listaTemasNoBanco);

        //ACT
        var resultado = await _temaService.GetAllAsync();

        //ASSERT
        resultado.Should().NotBeNullOrEmpty();    
        resultado.Should().HaveCount(2);            
        resultado.First().Nome.Should().Be("API ASP .NET Core");    
    }

    [Fact]
    public async Task CreateAsync_Sucesso_DeveRetornarTemaResponseDTO()
    {
        // ARRANGE
        var temaRequestDto = new TemaRequestDTO
        {
            Nome = "C#"
        };

        var temaCriado = new Tema
        {
            TemaId = 1,
            Nome = "C#"
        };

        _uofMock.Setup(u => u.TemaRepository.Create(It.IsAny<Tema>()))
                .Returns(temaCriado);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        // ACT
        var resultado = await _temaService.CreateAsync(temaRequestDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.TemaId.Should().Be(1);
        resultado.Nome.Should().Be("C#");

        _uofMock.Verify(u => u.TemaRepository.Create(It.IsAny<Tema>()), Times.Once);
        _uofMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_DadosInvalidos_DeveRetornarArgumentException()
    {
        // ARRANGE
        TemaRequestDTO? temaRequestDto = null;

        // ACT
        Func<Task> acao = async () => await _temaService.CreateAsync(temaRequestDto);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("Dados inválidos");

        _uofMock.Verify(u => u.TemaRepository.Create(It.IsAny<Tema>()), Times.Never);
        _uofMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_Sucesso_DeveRetornarTemaAtualizado()
    {
        // ARRANGE
        int temaId = 1;
        var temaUpdateDto = new TemaUpdateDTO
        {
            TemaId = temaId,
            Nome = "C# Avançado"
        };

        var temaExistente = new Tema
        {
            TemaId = temaId,
            Nome = "C#"
        };

        var temaAtualizado = new Tema
        {
            TemaId = temaId,
            Nome = "C# Avançado"
        };

        _uofMock.Setup(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()))
                .ReturnsAsync(temaExistente);

        _uofMock.Setup(u => u.TemaRepository.Update(It.IsAny<Tema>()))
                .Returns(temaAtualizado);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        // ACT
        var resultado = await _temaService.UpdateAsync(temaId, temaUpdateDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.TemaId.Should().Be(temaId);
        resultado.Nome.Should().Be("C# Avançado");

        _uofMock.Verify(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.TemaRepository.Update(It.IsAny<Tema>()), Times.Once);
        _uofMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_TemaNaoEncontrado_DeveRetornarKeyNotFoundException()
    {
        // ARRANGE
        int temaId = 999;
        var temaUpdateDto = new TemaUpdateDTO
        {
            TemaId = temaId,
            Nome = "Tema Inexistente"
        };

        _uofMock.Setup(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()))
                .ReturnsAsync((Tema?)null);

        // ACT
        Func<Task> acao = async () => await _temaService.UpdateAsync(temaId, temaUpdateDto);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Tema não encontrado.");

        _uofMock.Verify(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.TemaRepository.Update(It.IsAny<Tema>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_DadosInvalidos_DeveRetornarArgumentException()
    {
        // ARRANGE
        int temaId = 1;
        var temaUpdateDto = new TemaUpdateDTO
        {
            TemaId = 999, // ID diferente do parâmetro
            Nome = "C# Avançado"
        };

        // ACT
        Func<Task> acao = async () => await _temaService.UpdateAsync(temaId, temaUpdateDto);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("O ID da URL não coincide com o ID do corpo.");

        _uofMock.Verify(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()), Times.Never);
        _uofMock.Verify(u => u.TemaRepository.Update(It.IsAny<Tema>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_Sucesso_DeveExcluirTema()
    {
        // ARRANGE
        int temaId = 1;

        var temaExistente = new Tema
        {
            TemaId = temaId,
            Nome = "C#"
        };

        _uofMock.Setup(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()))
                .ReturnsAsync(temaExistente);

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync((BlogPessoal.Models.Postagem?)null);

        _uofMock.Setup(u => u.TemaRepository.Delete(It.IsAny<Tema>()))
                .Returns(temaExistente);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        // ACT
        Func<Task> acao = async () => await _temaService.DeleteAsync(temaId);

        // ASSERT
        await acao.Should().NotThrowAsync();

        _uofMock.Verify(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.TemaRepository.Delete(It.IsAny<Tema>()), Times.Once);
        _uofMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_TemaNaoEncontrado_DeveRetornarKeyNotFoundException()
    {
        // ARRANGE
        int temaId = 999;

        _uofMock.Setup(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()))
                .ReturnsAsync((Tema?)null);

        // ACT
        Func<Task> acao = async () => await _temaService.DeleteAsync(temaId);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Tema não encontrado.");

        // Verifica que a lógica parou aqui e não continuou
        _uofMock.Verify(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Never);
        _uofMock.Verify(u => u.TemaRepository.Delete(It.IsAny<Tema>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_PostagemVinculada_DeveRetornarInvalidOperationException()
    {
        // ARRANGE
        int temaId = 1;

        var temaExistente = new Tema
        {
            TemaId = temaId,
            Nome = "C#"
        };

        var postagemVinculada = new BlogPessoal.Models.Postagem
        {
            PostagemId = 1,
            Titulo = "Post",
            Texto = "Conteúdo",
            TemaId = temaId
        };

        _uofMock.Setup(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()))
                .ReturnsAsync(temaExistente);

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemVinculada);

        // ACT
        Func<Task> acao = async () => await _temaService.DeleteAsync(temaId);

        // ASSERT
        await acao.Should().ThrowAsync<InvalidOperationException>()
                  .WithMessage("Existem postagens vinculadas ao Tema.");

        // Verifica que a lógica parou antes de chamar Delete
        _uofMock.Verify(u => u.TemaRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tema, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.TemaRepository.Delete(It.IsAny<Tema>()), Times.Never);
    }
}