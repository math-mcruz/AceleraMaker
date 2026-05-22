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

    #region GetAllAsync Tests

    [Fact]
    public async Task GetAllAsync_ListaVazia_DeveRetornarKeyNotFoundException()
    {
        // ARRANGE
        _uofMock.Setup(u => u.PostagemRepository.GetAllAsync())
                .ReturnsAsync(new List<BlogPessoal.Models.Postagem>());

        // ACT
        Func<Task> acao = async () => await _postagemService.GetAllAsync();

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Não existem postagens criadas.");

        _uofMock.Verify(u => u.PostagemRepository.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ListaPreenchida_DeveRetornarPostagensDTO()
    {
        // ARRANGE
        var usuario = new BlogPessoal.Models.Usuario 
        { 
            Id = 1, 
            UserName = "usuario1" 
        };

        var tema = new BlogPessoal.Models.Tema 
        { 
            TemaId = 1, 
            Nome = "C#" 
        };

        var postagens = new List<BlogPessoal.Models.Postagem>
        {
            new BlogPessoal.Models.Postagem
            {
                PostagemId = 1,
                Titulo = "Post 1",
                Texto = "Texto 1",
                Data = DateTime.Now,
                UsuarioId = 1,
                Usuario = usuario,
                TemaId = 1,
                Tema = tema
            },
            new BlogPessoal.Models.Postagem
            {
                PostagemId = 2,
                Titulo = "Post 2",
                Texto = "Texto 2",
                Data = DateTime.Now,
                UsuarioId = 1,
                Usuario = usuario,
                TemaId = 1,
                Tema = tema
            }
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAllAsync())
                .ReturnsAsync(postagens);

        // ACT
        var resultado = await _postagemService.GetAllAsync();

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Should().HaveCount(2);
        resultado.First().Titulo.Should().Be("Post 1");
        resultado.Last().Titulo.Should().Be("Post 2");

        _uofMock.Verify(u => u.PostagemRepository.GetAllAsync(), Times.Once);
    }

    #endregion

    #region UpdateAsync Tests

    [Fact]
    public async Task UpdateAsync_Sucesso_DeveAtualizarPostagemDoAutor()
    {
        // ARRANGE
        int postagemId = 1;
        int usuarioLogadoId = 1;
        bool ehAdmin = false;

        var usuario = new BlogPessoal.Models.Usuario
        {
            Id = usuarioLogadoId,
            UserName = "autor"
        };

        var tema = new BlogPessoal.Models.Tema
        {
            TemaId = 1,
            Nome = "C#"
        };

        var postagemExistente = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post Antigo",
            Texto = "Texto Antigo",
            Data = DateTime.Now,
            UsuarioId = usuarioLogadoId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        var updateDto = new PostagemUpdateDTO
        {
            PostagemId = postagemId,
            Titulo = "Post Atualizado",
            Texto = "Texto Atualizado",
            Data = DateTime.Now,
            TemaId = 1
        };

        var postagemAtualizada = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post Atualizado",
            Texto = "Texto Atualizado",
            Data = updateDto.Data,
            UsuarioId = usuarioLogadoId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemExistente);

        _uofMock.Setup(u => u.PostagemRepository.Update(It.IsAny<BlogPessoal.Models.Postagem>()))
                .Returns(postagemAtualizada);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        // Mock para a segunda busca após atualização
        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemAtualizada);

        // ACT
        var resultado = await _postagemService.UpdateAsync(postagemId, updateDto, usuarioLogadoId, ehAdmin);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.PostagemId.Should().Be(postagemId);
        resultado.Titulo.Should().Be("Post Atualizado");
        resultado.Texto.Should().Be("Texto Atualizado");
        resultado.UsuarioId.Should().Be(usuarioLogadoId);

        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Exactly(2));
        _uofMock.Verify(u => u.PostagemRepository.Update(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Once);
        _uofMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_AcessoNegado_DeveRetornarUnauthorizedAccessException()
    {
        // ARRANGE
        int postagemId = 1;
        int usuarioLogadoId = 2;
        int autorId = 1;
        bool ehAdmin = false;

        var usuario = new BlogPessoal.Models.Usuario
        {
            Id = autorId,
            UserName = "autor"
        };

        var tema = new BlogPessoal.Models.Tema
        {
            TemaId = 1,
            Nome = "C#"
        };

        var postagemExistente = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post do Outro",
            Texto = "Texto",
            Data = DateTime.Now,
            UsuarioId = autorId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        var updateDto = new PostagemUpdateDTO
        {
            PostagemId = postagemId,
            Titulo = "Post Atualizado",
            Texto = "Texto Atualizado",
            Data = DateTime.Now,
            TemaId = 1
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemExistente);

        // ACT
        Func<Task> acao = async () => await _postagemService.UpdateAsync(postagemId, updateDto, usuarioLogadoId, ehAdmin);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Sem permissão para editar a postagem.");

        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.Update(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_AdminAtualizandoPostageDeOutro_DeveAtualizarComSucesso()
    {
        // ARRANGE
        int postagemId = 1;
        int usuarioLogadoId = 2;
        int autorId = 1;
        bool ehAdmin = true;

        var usuario = new BlogPessoal.Models.Usuario
        {
            Id = autorId,
            UserName = "autor"
        };

        var tema = new BlogPessoal.Models.Tema
        {
            TemaId = 1,
            Nome = "C#"
        };

        var postagemExistente = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post do Outro",
            Texto = "Texto",
            Data = DateTime.Now,
            UsuarioId = autorId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        var updateDto = new PostagemUpdateDTO
        {
            PostagemId = postagemId,
            Titulo = "Post Atualizado",
            Texto = "Texto Atualizado",
            Data = DateTime.Now,
            TemaId = 1
        };

        var postagemAtualizada = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post Atualizado",
            Texto = "Texto Atualizado",
            Data = updateDto.Data,
            UsuarioId = autorId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemExistente);

        _uofMock.Setup(u => u.PostagemRepository.Update(It.IsAny<BlogPessoal.Models.Postagem>()))
                .Returns(postagemAtualizada);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemAtualizada);

        // ACT
        var resultado = await _postagemService.UpdateAsync(postagemId, updateDto, usuarioLogadoId, ehAdmin);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado.Titulo.Should().Be("Post Atualizado");

        _uofMock.Verify(u => u.PostagemRepository.Update(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_PostagemNaoEncontrada_DeveRetornarKeyNotFoundException()
    {
        // ARRANGE
        int postagemId = 999;
        int usuarioLogadoId = 1;
        bool ehAdmin = false;

        var updateDto = new PostagemUpdateDTO
        {
            PostagemId = postagemId,
            Titulo = "Post",
            Texto = "Texto",
            Data = DateTime.Now,
            TemaId = 1
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync((BlogPessoal.Models.Postagem?)null);

        // ACT
        Func<Task> acao = async () => await _postagemService.UpdateAsync(postagemId, updateDto, usuarioLogadoId, ehAdmin);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Postagem não encontrada.");

        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.Update(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_DadosInvalidos_DeveRetornarArgumentException()
    {
        // ARRANGE
        int postagemId = 1;
        int postIdDto = 999; // IDs diferentes
        int usuarioLogadoId = 1;
        bool ehAdmin = false;

        var updateDto = new PostagemUpdateDTO
        {
            PostagemId = postIdDto,
            Titulo = "Post",
            Texto = "Texto",
            Data = DateTime.Now,
            TemaId = 1
        };

        // ACT
        Func<Task> acao = async () => await _postagemService.UpdateAsync(postagemId, updateDto, usuarioLogadoId, ehAdmin);

        // ASSERT
        await acao.Should().ThrowAsync<ArgumentException>()
                  .WithMessage("Dados inválidos.");

        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Never);
    }

    #endregion

    #region DeleteAsync Tests

    [Fact]
    public async Task DeleteAsync_Sucesso_DeveExcluirPostageDoAutor()
    {
        // ARRANGE
        int postagemId = 1;
        int usuarioLogadoId = 1;
        bool ehAdmin = false;

        var usuario = new BlogPessoal.Models.Usuario
        {
            Id = usuarioLogadoId,
            UserName = "autor"
        };

        var tema = new BlogPessoal.Models.Tema
        {
            TemaId = 1,
            Nome = "C#"
        };

        var postagemExistente = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post para Deletar",
            Texto = "Texto",
            Data = DateTime.Now,
            UsuarioId = usuarioLogadoId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemExistente);

        _uofMock.Setup(u => u.PostagemRepository.Delete(It.IsAny<BlogPessoal.Models.Postagem>()))
                .Returns(postagemExistente);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        // ACT
        Func<Task> acao = async () => await _postagemService.DeleteAsync(postagemId, usuarioLogadoId, ehAdmin);

        // ASSERT
        await acao.Should().NotThrowAsync();

        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.Delete(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Once);
        _uofMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_AcessoNegado_DeveRetornarUnauthorizedAccessException()
    {
        // ARRANGE
        int postagemId = 1;
        int usuarioLogadoId = 2;
        int autorId = 1;
        bool ehAdmin = false;

        var usuario = new BlogPessoal.Models.Usuario
        {
            Id = autorId,
            UserName = "autor"
        };

        var tema = new BlogPessoal.Models.Tema
        {
            TemaId = 1,
            Nome = "C#"
        };

        var postagemExistente = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post do Outro",
            Texto = "Texto",
            Data = DateTime.Now,
            UsuarioId = autorId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemExistente);

        // ACT
        Func<Task> acao = async () => await _postagemService.DeleteAsync(postagemId, usuarioLogadoId, ehAdmin);

        // ASSERT
        await acao.Should().ThrowAsync<UnauthorizedAccessException>()
                  .WithMessage("Sem permissão para excluir a postagem.");

        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.Delete(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_AdminDeletandoPostageDeOutro_DeveExcluirComSucesso()
    {
        // ARRANGE
        int postagemId = 1;
        int usuarioLogadoId = 2;
        int autorId = 1;
        bool ehAdmin = true;

        var usuario = new BlogPessoal.Models.Usuario
        {
            Id = autorId,
            UserName = "autor"
        };

        var tema = new BlogPessoal.Models.Tema
        {
            TemaId = 1,
            Nome = "C#"
        };

        var postagemExistente = new BlogPessoal.Models.Postagem
        {
            PostagemId = postagemId,
            Titulo = "Post do Outro",
            Texto = "Texto",
            Data = DateTime.Now,
            UsuarioId = autorId,
            Usuario = usuario,
            TemaId = 1,
            Tema = tema
        };

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync(postagemExistente);

        _uofMock.Setup(u => u.PostagemRepository.Delete(It.IsAny<BlogPessoal.Models.Postagem>()))
                .Returns(postagemExistente);

        _uofMock.Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);

        // ACT
        Func<Task> acao = async () => await _postagemService.DeleteAsync(postagemId, usuarioLogadoId, ehAdmin);

        // ASSERT
        await acao.Should().NotThrowAsync();

        _uofMock.Verify(u => u.PostagemRepository.Delete(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_PostagemNaoEncontrada_DeveRetornarKeyNotFoundException()
    {
        // ARRANGE
        int postagemId = 999;
        int usuarioLogadoId = 1;
        bool ehAdmin = false;

        _uofMock.Setup(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()))
                .ReturnsAsync((BlogPessoal.Models.Postagem?)null);

        // ACT
        Func<Task> acao = async () => await _postagemService.DeleteAsync(postagemId, usuarioLogadoId, ehAdmin);

        // ASSERT
        await acao.Should().ThrowAsync<KeyNotFoundException>()
                  .WithMessage("Postagem não encontrada.");

        _uofMock.Verify(u => u.PostagemRepository.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<BlogPessoal.Models.Postagem, bool>>>()), Times.Once);
        _uofMock.Verify(u => u.PostagemRepository.Delete(It.IsAny<BlogPessoal.Models.Postagem>()), Times.Never);
    }

    #endregion

}
