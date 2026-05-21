using BlogPessoal.Controllers;
using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Services.Tema;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace BlogPessoal.Tests.TemaTests;

public class TemasControllerTests
{
    private readonly Mock<ITemaService> _mockTemaService;
    private readonly TemasController _temasController;

    public TemasControllerTests()
    {
        _mockTemaService = new Mock<ITemaService>();
        _temasController = new TemasController(_mockTemaService.Object);
    }

    private void ConfigurarUsuarioAutenticado(int usuarioId, bool ehAdmin = false)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
            new Claim(ClaimTypes.Name, "usuario_teste")
        };

        if (ehAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = principal };
        _temasController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }  

    [Fact]
    public async Task Get_RetornaOkComLista()
    {
        // ARRANGE
        var temasEsperados = new List<TemaResponseDTO>
        {
            new TemaResponseDTO
            {
                TemaId = 1,
                Nome = "C#"
            },
            new TemaResponseDTO
            {
                TemaId = 2,
                Nome = "JavaScript"
            },
            new TemaResponseDTO
            {
                TemaId = 3,
                Nome = "Python"
            }
        };

        _mockTemaService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(temasEsperados);

        // ACT
        var resultado = await _temasController.Get();

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);
        okResult?.Value.Should().BeEquivalentTo(temasEsperados);

        _mockTemaService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Get_RetornaOkComListaVazia()
    {
        // ARRANGE
        var temasVazios = new List<TemaResponseDTO>();

        _mockTemaService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(temasVazios);

        // ACT
        var resultado = await _temasController.Get();

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);

        var resultadoList = okResult?.Value as List<TemaResponseDTO>;
        resultadoList.Should().BeEmpty();

        _mockTemaService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Get_ThrowsKeyNotFoundException()
    {
        // ARRANGE
        _mockTemaService.Setup(s => s.GetAllAsync())
                        .ThrowsAsync(new KeyNotFoundException("Não existem temas criados"));

        // ACT
        Func<Task> acao = async () => await _temasController.Get();

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Não existem temas criados");

        _mockTemaService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Post_Retorna201Created_ComTemaCriado()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var temaRequestDto = new TemaRequestDTO
        {
            Nome = "TypeScript"
        };

        var temaCriado = new TemaResponseDTO
        {
            TemaId = 4,
            Nome = "TypeScript"
        };

        _mockTemaService.Setup(s => s.CreateAsync(temaRequestDto))
                        .ReturnsAsync(temaCriado);

        // ACT
        var resultado = await _temasController.Post(temaRequestDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<ObjectResult>();

        var objectResult = resultado.Result as ObjectResult;
        objectResult?.StatusCode.Should().Be(StatusCodes.Status201Created);
        objectResult?.Value.Should().Be(temaCriado);

        _mockTemaService.Verify(s => s.CreateAsync(temaRequestDto), Times.Once);
    }

    [Fact]
    public async Task Post_ThrowsArgumentException_ComDadosInvalidos()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var temaRequestDtoInvalida = new TemaRequestDTO
        {
            Nome = null
        };

        _mockTemaService.Setup(s => s.CreateAsync(temaRequestDtoInvalida))
                        .ThrowsAsync(new ArgumentException("Nome é obrigatório."));

        // ACT
        Func<Task> acao = async () => await _temasController.Post(temaRequestDtoInvalida);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("Nome é obrigatório.");

        _mockTemaService.Verify(s => s.CreateAsync(temaRequestDtoInvalida), Times.Once);
    }

    [Fact]
    public async Task Post_ThrowsInvalidOperationException_TemaJaExiste()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var temaRequestDto = new TemaRequestDTO
        {
            Nome = "C#"
        };

        _mockTemaService.Setup(s => s.CreateAsync(temaRequestDto))
                        .ThrowsAsync(new InvalidOperationException("Tema já existe."));

        // ACT
        Func<Task> acao = async () => await _temasController.Post(temaRequestDto);

        // ASSERT
        await acao.Should().ThrowAsync<InvalidOperationException>()
                  .WithMessage("Tema já existe.");

        _mockTemaService.Verify(s => s.CreateAsync(temaRequestDto), Times.Once);
    }

    [Fact]
    public async Task Put_RetornaOk_ComTemaAtualizado()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var id = 1;
        var temaUpdateDto = new TemaUpdateDTO
        {
            TemaId = id,
            Nome = "C# Avançado"
        };

        var temaAtualizado = new TemaResponseDTO
        {
            TemaId = id,
            Nome = "C# Avançado"
        };

        _mockTemaService.Setup(s => s.UpdateAsync(id, temaUpdateDto))
                        .ReturnsAsync(temaAtualizado);

        // ACT
        var resultado = await _temasController.Put(id, temaUpdateDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);
        okResult?.Value.Should().Be(temaAtualizado);

        _mockTemaService.Verify(s => s.UpdateAsync(id, temaUpdateDto), Times.Once);
    }

    [Fact]
    public async Task Put_ThrowsKeyNotFoundException_TemaNaoExiste()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var id = 999;
        var temaUpdateDto = new TemaUpdateDTO
        {
            TemaId = id,
            Nome = "Tema Inexistente"
        };

        _mockTemaService.Setup(s => s.UpdateAsync(id, temaUpdateDto))
                        .ThrowsAsync(new KeyNotFoundException("Tema não encontrado."));

        // ACT
        Func<Task> acao = async () => await _temasController.Put(id, temaUpdateDto);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Tema não encontrado.");

        _mockTemaService.Verify(s => s.UpdateAsync(id, temaUpdateDto), Times.Once);
    }

    [Fact]
    public async Task Put_ThrowsArgumentException_DadosInvalidos()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var id = 1;
        var temaUpdateDto = new TemaUpdateDTO
        {
            TemaId = 2, // ID mismatch
            Nome = "Tema Atualizado"
        };

        _mockTemaService.Setup(s => s.UpdateAsync(id, temaUpdateDto))
                        .ThrowsAsync(new ArgumentException("Dados inválidos."));

        // ACT
        Func<Task> acao = async () => await _temasController.Put(id, temaUpdateDto);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("Dados inválidos.");

        _mockTemaService.Verify(s => s.UpdateAsync(id, temaUpdateDto), Times.Once);
    }

    [Fact]
    public async Task Delete_RetornaNoContent_ExclusaoComSucesso_Admin()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var id = 1;

        _mockTemaService.Setup(s => s.DeleteAsync(id))
                        .Returns(Task.CompletedTask);

        // ACT
        var resultado = await _temasController.Delete(id);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<NoContentResult>();

        var noContentResult = resultado.Result as NoContentResult;
        noContentResult?.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockTemaService.Verify(s => s.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task Delete_ThrowsKeyNotFoundException_TemaNaoExiste()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var id = 999;

        _mockTemaService.Setup(s => s.DeleteAsync(id))
                        .ThrowsAsync(new KeyNotFoundException("Tema não encontrado."));

        // ACT
        Func<Task> acao = async () => await _temasController.Delete(id);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Tema não encontrado.");

        _mockTemaService.Verify(s => s.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task Delete_RetornaNoContent_UsuarioNaoAdmin()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 2, ehAdmin: false);

        var id = 1;

        _mockTemaService.Setup(s => s.DeleteAsync(id))
                        .Returns(Task.CompletedTask);

        // ACT
        var resultado = await _temasController.Delete(id);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<NoContentResult>();

        var noContentResult = resultado.Result as NoContentResult;
        noContentResult?.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockTemaService.Verify(s => s.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task Delete_DuasPermissoes_AdminVsUsuarioComum()
    {
        // ARRANGE 
        var id = 1;

        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);
        _mockTemaService.Setup(s => s.DeleteAsync(id))
                        .Returns(Task.CompletedTask);

        var resultadoAdmin = await _temasController.Delete(id);

        resultadoAdmin.Result.Should().BeOfType<NoContentResult>();
        var noContentResultAdmin = resultadoAdmin.Result as NoContentResult;
        noContentResultAdmin?.StatusCode.Should().Be(StatusCodes.Status204NoContent);


        ConfigurarUsuarioAutenticado(usuarioId: 2, ehAdmin: false);
        _mockTemaService.Reset();
        _mockTemaService.Setup(s => s.DeleteAsync(id))
                        .Returns(Task.CompletedTask);

        var resultadoUsuarioComum = await _temasController.Delete(id);

        resultadoUsuarioComum.Result.Should().BeOfType<NoContentResult>();
        var noContentResultUsuario = resultadoUsuarioComum.Result as NoContentResult;
        noContentResultUsuario?.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        // ASSERT
    }
}