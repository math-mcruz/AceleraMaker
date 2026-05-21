using BlogPessoal.Models;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services.Temas;
using FluentAssertions;
using Moq;

namespace BlogPessoal.Tests;

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
}
