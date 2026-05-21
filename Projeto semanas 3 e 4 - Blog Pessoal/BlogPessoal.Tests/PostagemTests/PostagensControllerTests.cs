using BlogPessoal.Controllers;
using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models.Pagination;
using BlogPessoal.Services.Postagens;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace BlogPessoal.Tests.PostagemTests;

public class PostagensControllerTests
{
    private readonly Mock<IPostagemService> _mockPostagemService;
    private readonly PostagensController _postagensController;

    public PostagensControllerTests()
    {
        _mockPostagemService = new Mock<IPostagemService>();
        _postagensController = new PostagensController(_mockPostagemService.Object);
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
        _postagensController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public async Task Get_RetornaOkComLista()
    {
        // ARRANGE
        var postagensEsperadas = new List<PostagemResponseDTO>
        {
            new PostagemResponseDTO
            {
                PostagemId = 1,
                Titulo = "Primeira Postagem",
                Texto = "Conteúdo da primeira postagem",
                TemaId = 1,
                UsuarioId = 1,
                NomeAutor = "usuario1",
                Data = DateTime.Now
            },
            new PostagemResponseDTO
            {
                PostagemId = 2,
                Titulo = "Segunda Postagem",
                Texto = "Conteúdo da segunda postagem",
                TemaId = 2,
                UsuarioId = 2,
                NomeAutor = "usuario2",
                Data = DateTime.Now
            }
        };

        _mockPostagemService.Setup(s => s.GetAllAsync())
                            .ReturnsAsync(postagensEsperadas);

        // ACT
        var resultado = await _postagensController.Get();

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);
        okResult?.Value.Should().BeEquivalentTo(postagensEsperadas);

        _mockPostagemService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Get_RetornaOkComListaVazia()
    {
        // ARRANGE
        var postagensVazias = new List<PostagemResponseDTO>();

        _mockPostagemService.Setup(s => s.GetAllAsync())
                            .ReturnsAsync(postagensVazias);

        // ACT
        var resultado = await _postagensController.Get();

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);

        var resultadoList = okResult?.Value as List<PostagemResponseDTO>;
        resultadoList.Should().BeEmpty();

        _mockPostagemService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Get_ThrowsKeyNotFoundException()
    {
        // ARRANGE
        _mockPostagemService.Setup(s => s.GetAllAsync())
                            .ThrowsAsync(new KeyNotFoundException("Não existem postagens criadas."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Get();

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Não existem postagens criadas.");

        _mockPostagemService.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetFiltro_RetornaOkComPostagensFiltradasPorAutor()
    {
        // ARRANGE
        var filtro = new PostagensFiltroAutorTema { AutorId = 1 };

        var postagensFiltradasPagedResponse = new PagedResponse<PostagemResponseDTO>(
            dados: new List<PostagemResponseDTO>
            {
                new PostagemResponseDTO
                {
                    PostagemId = 1,
                    Titulo = "Postagem do Usuario 1",
                    Texto = "Conteúdo",
                    TemaId = 1,
                    UsuarioId = 1,
                    NomeAutor = "usuario1",
                    Data = DateTime.Now
                }
            },
            count: 1,
            pageNumber: 1,
            pageSize: 10
        );

        _mockPostagemService.Setup(s => s.GetPorFiltroAsync(filtro))
                            .ReturnsAsync(postagensFiltradasPagedResponse);

        // ACT
        var resultado = await _postagensController.GetFiltro(filtro);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);
        okResult?.Value.Should().Be(postagensFiltradasPagedResponse);

        _mockPostagemService.Verify(s => s.GetPorFiltroAsync(filtro), Times.Once);
    }

    [Fact]
    public async Task GetFiltro_RetornaOkComPostagensFiltradasPorTema()
    {
        // ARRANGE
        var filtro = new PostagensFiltroAutorTema { TemaId = 2 };

        var postagensFiltradasPagedResponse = new PagedResponse<PostagemResponseDTO>(
            dados: new List<PostagemResponseDTO>
            {
                new PostagemResponseDTO
                {
                    PostagemId = 2,
                    Titulo = "Postagem sobre Tema 2",
                    Texto = "Conteúdo",
                    TemaId = 2,
                    UsuarioId = 1,
                    NomeAutor = "usuario1",
                    Data = DateTime.Now
                }
            },
            count: 1,
            pageNumber: 1,
            pageSize: 10
        );

        _mockPostagemService.Setup(s => s.GetPorFiltroAsync(filtro))
                            .ReturnsAsync(postagensFiltradasPagedResponse);

        // ACT
        var resultado = await _postagensController.GetFiltro(filtro);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);

        _mockPostagemService.Verify(s => s.GetPorFiltroAsync(filtro), Times.Once);
    }

    [Fact]
    public async Task GetFiltro_ThrowsKeyNotFoundException()
    {
        // ARRANGE
        var filtro = new PostagensFiltroAutorTema { AutorId = 999 };

        _mockPostagemService.Setup(s => s.GetPorFiltroAsync(filtro))
                            .ThrowsAsync(new KeyNotFoundException("Não existem postagens criadas com o filtro aplicado."));

        // ACT
        Func<Task> acao = async () => await _postagensController.GetFiltro(filtro);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>();

        _mockPostagemService.Verify(s => s.GetPorFiltroAsync(filtro), Times.Once);
    }

    [Fact]
    public async Task Post_Retorna201Created_ComPostagemCriada()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1);

        var postRequestDto = new PostagemRequestDTO
        {
            Titulo = "Nova Postagem",
            Texto = "Conteúdo da nova postagem",
            TemaId = 1
        };

        var postagemCriada = new PostagemResponseDTO
        {
            PostagemId = 1,
            Titulo = "Nova Postagem",
            Texto = "Conteúdo da nova postagem",
            TemaId = 1,
            UsuarioId = 1,
            NomeAutor = "usuario_teste",
            Data = DateTime.Now
        };

        _mockPostagemService.Setup(s => s.CreateAsync(postRequestDto, 1))
                            .ReturnsAsync(postagemCriada);

        // ACT
        var resultado = await _postagensController.Post(postRequestDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<ObjectResult>();

        var objectResult = resultado.Result as ObjectResult;
        objectResult?.StatusCode.Should().Be(StatusCodes.Status201Created);
        objectResult?.Value.Should().Be(postagemCriada);

        _mockPostagemService.Verify(s => s.CreateAsync(postRequestDto, 1), Times.Once);
    }

    [Fact]
    public async Task Post_ThrowsArgumentException_ComDadosInvalidos()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1);

        var postRequestDtoInvalida = new PostagemRequestDTO
        {
            Titulo = null,
            Texto = null,
            TemaId = 0
        };

        _mockPostagemService.Setup(s => s.CreateAsync(postRequestDtoInvalida, 1))
                            .ThrowsAsync(new ArgumentException("Dados inválidos."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Post(postRequestDtoInvalida);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("Dados inválidos.");

        _mockPostagemService.Verify(s => s.CreateAsync(postRequestDtoInvalida, 1), Times.Once);
    }

    [Fact]
    public async Task Post_ThrowsKeyNotFoundException_ComTemaInvalido()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1);

        var postRequestDto = new PostagemRequestDTO
        {
            Titulo = "Postagem com Tema Inválido",
            Texto = "Conteúdo",
            TemaId = 999
        };

        _mockPostagemService.Setup(s => s.CreateAsync(postRequestDto, 1))
                            .ThrowsAsync(new KeyNotFoundException("Tema não encontrado."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Post(postRequestDto);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Tema não encontrado.");

        _mockPostagemService.Verify(s => s.CreateAsync(postRequestDto, 1), Times.Once);
    }

    [Fact]
    public async Task Put_RetornaOk_ComPostagemAtualizada()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: false);

        var id = 1;
        var postUpdateDto = new PostagemUpdateDTO
        {
            PostagemId = id,
            Titulo = "Postagem Atualizada",
            Texto = "Conteúdo atualizado",
            TemaId = 2,
            Data = DateTime.Now
        };

        var postagemAtualizada = new PostagemResponseDTO
        {
            PostagemId = id,
            Titulo = "Postagem Atualizada",
            Texto = "Conteúdo atualizado",
            TemaId = 2,
            UsuarioId = 1,
            NomeAutor = "usuario_teste",
            Data = DateTime.Now
        };

        _mockPostagemService.Setup(s => s.UpdateAsync(id, postUpdateDto, 1, false))
                            .ReturnsAsync(postagemAtualizada);

        // ACT
        var resultado = await _postagensController.Put(id, postUpdateDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);
        okResult?.Value.Should().Be(postagemAtualizada);

        _mockPostagemService.Verify(s => s.UpdateAsync(id, postUpdateDto, 1, false), Times.Once);
    }

    [Fact]
    public async Task Put_RetornaOk_AdminAtualizandoPostagemDeOutroUsuario()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var id = 2;
        var postUpdateDto = new PostagemUpdateDTO
        {
            PostagemId = id,
            Titulo = "Postagem Atualizada por Admin",
            Texto = "Conteúdo atualizado",
            TemaId = 1,
            Data = DateTime.Now
        };

        var postagemAtualizada = new PostagemResponseDTO
        {
            PostagemId = id,
            Titulo = "Postagem Atualizada por Admin",
            Texto = "Conteúdo atualizado",
            TemaId = 1,
            UsuarioId = 2,
            NomeAutor = "outro_usuario",
            Data = DateTime.Now
        };

        _mockPostagemService.Setup(s => s.UpdateAsync(id, postUpdateDto, 1, true))
                            .ReturnsAsync(postagemAtualizada);

        // ACT
        var resultado = await _postagensController.Put(id, postUpdateDto);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<OkObjectResult>();

        var okResult = resultado.Result as OkObjectResult;
        okResult?.StatusCode.Should().Be(200);

        _mockPostagemService.Verify(s => s.UpdateAsync(id, postUpdateDto, 1, true), Times.Once);
    }

    [Fact]
    public async Task Put_ThrowsUnauthorizedAccessException_UsuarioSemPermissao()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: false);

        var id = 3; // Postagem de outro usuário
        var postUpdateDto = new PostagemUpdateDTO
        {
            PostagemId = id,
            Titulo = "Tentativa de Atualização",
            Texto = "Conteúdo",
            TemaId = 1,
            Data = DateTime.Now
        };

        _mockPostagemService.Setup(s => s.UpdateAsync(id, postUpdateDto, 1, false))
                            .ThrowsAsync(new UnauthorizedAccessException("Sem permissão para editar a postagem."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Put(id, postUpdateDto);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Sem permissão para editar a postagem.");

        _mockPostagemService.Verify(s => s.UpdateAsync(id, postUpdateDto, 1, false), Times.Once);
    }

    [Fact]
    public async Task Put_ThrowsKeyNotFoundException_PostagemNaoExiste()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1);

        var id = 999;
        var postUpdateDto = new PostagemUpdateDTO
        {
            PostagemId = id,
            Titulo = "Postagem Inexistente",
            Texto = "Conteúdo",
            TemaId = 1,
            Data = DateTime.Now
        };

        _mockPostagemService.Setup(s => s.UpdateAsync(id, postUpdateDto, 1, false))
                            .ThrowsAsync(new KeyNotFoundException("Postagem não encontrada."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Put(id, postUpdateDto);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Postagem não encontrada.");

        _mockPostagemService.Verify(s => s.UpdateAsync(id, postUpdateDto, 1, false), Times.Once);
    }

    [Fact]
    public async Task Put_ThrowsArgumentException_DadosInvalidos()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1);

        var id = 1;
        var postUpdateDto = new PostagemUpdateDTO
        {
            PostagemId = 2, // ID mismatch
            Titulo = "Postagem",
            Texto = "Conteúdo",
            TemaId = 1,
            Data = DateTime.Now
        };

        _mockPostagemService.Setup(s => s.UpdateAsync(id, postUpdateDto, 1, false))
                            .ThrowsAsync(new ArgumentException("Dados inválidos."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Put(id, postUpdateDto);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("Dados inválidos.");

        _mockPostagemService.Verify(s => s.UpdateAsync(id, postUpdateDto, 1, false), Times.Once);
    }

    [Fact]
    public async Task Delete_RetornaNoContent_ExclusaoComSucesso()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1);

        var id = 1;

        _mockPostagemService.Setup(s => s.DeleteAsync(id, 1, false))
                            .Returns(Task.CompletedTask);

        // ACT
        var resultado = await _postagensController.Delete(id);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<NoContentResult>();

        var noContentResult = resultado.Result as NoContentResult;
        noContentResult?.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockPostagemService.Verify(s => s.DeleteAsync(id, 1, false), Times.Once);
    }

    [Fact]
    public async Task Delete_RetornaNoContent_AdminExcluindoPostagemDeOutroUsuario()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: true);

        var id = 2;

        _mockPostagemService.Setup(s => s.DeleteAsync(id, 1, true))
                            .Returns(Task.CompletedTask);

        // ACT
        var resultado = await _postagensController.Delete(id);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Result.Should().BeOfType<NoContentResult>();

        var noContentResult = resultado.Result as NoContentResult;
        noContentResult?.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockPostagemService.Verify(s => s.DeleteAsync(id, 1, true), Times.Once);
    }

    [Fact]
    public async Task Delete_ThrowsUnauthorizedAccessException_UsuarioSemPermissao()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1, ehAdmin: false);

        var id = 3; // Postagem de outro usuário

        _mockPostagemService.Setup(s => s.DeleteAsync(id, 1, false))
                            .ThrowsAsync(new UnauthorizedAccessException("Sem permissão para excluir a postagem."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Delete(id);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Sem permissão para excluir a postagem.");

        _mockPostagemService.Verify(s => s.DeleteAsync(id, 1, false), Times.Once);
    }

    [Fact]
    public async Task Delete_ThrowsKeyNotFoundException_PostagemNaoExiste()
    {
        // ARRANGE
        ConfigurarUsuarioAutenticado(usuarioId: 1);

        var id = 999;

        _mockPostagemService.Setup(s => s.DeleteAsync(id, 1, false))
                            .ThrowsAsync(new KeyNotFoundException("Postagem não encontrada."));

        // ACT
        Func<Task> acao = async () => await _postagensController.Delete(id);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Postagem não encontrada.");

        _mockPostagemService.Verify(s => s.DeleteAsync(id, 1, false), Times.Once);
    }
}