using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models.Pagination;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services.IA;

namespace BlogPessoal.Services.Postagens;

public class PostagemService : IPostagemService
{
    private readonly IUnitOfWork _uof;
    private readonly IIAService _iaService;
    public PostagemService(IUnitOfWork uof, IIAService iaService)
    {
        _uof = uof;
        _iaService = iaService;
    }

    public async Task<IEnumerable<PostagemResponseDTO>> GetAllAsync()
    {
        var postagens = await _uof.PostagemRepository.GetAllAsync();

        if (postagens is null || !postagens.Any())
            throw new KeyNotFoundException("Não existem postagens criadas.");

        return postagens.ToPostagemDTOList();
    }

    public async Task<PagedResponse<PostagemResponseDTO>> GetPorFiltroAsync(PostagensFiltroAutorTema postFiltro)
    {
        var postagensPaginadas = await _uof.PostagemRepository.GetFiltroAutorTemaAsync(postFiltro);

        if (postagensPaginadas.Dados is null || !postagensPaginadas.Dados.Any())
            throw new KeyNotFoundException("Não existem postagens criadas com o filtro aplicado.");

        var postagensDto = postagensPaginadas.Dados.ToPostagemDTOList();

        return new PagedResponse<PostagemResponseDTO>(
            dados: postagensDto,
            count: postagensPaginadas.TotalCount,
            pageNumber: postagensPaginadas.CurrentPage,
            pageSize: postagensPaginadas.PageSize
        );
    }

    public async Task<PostagemResponseDTO> CreateAsync(PostagemRequestDTO postRequestDto, int usuarioLogadoId)
    {
        if (postRequestDto is null)
            throw new ArgumentException("Dados inválidos.");

        var temaExiste = await _uof.TemaRepository.GetAsync(t => t.TemaId == postRequestDto.TemaId);
        if (temaExiste is null)
            throw new KeyNotFoundException("Tema não encontrado."); 

        var post = postRequestDto.RequestToPost(usuarioLogadoId);

        var postIA = await _iaService.GerarResumoAsync(post.Texto);
        post.ResumoIA = postIA.Resumo;
        post.TagsIA = postIA.Tags;
        post.CategoriaIA = postIA.Categoria;

        var postCriado = _uof.PostagemRepository.Create(post);
        await _uof.CommitAsync();

        var postCompleto = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == postCriado.PostagemId);

        return postCompleto.ToPostResponseDTO();
    }

    public async Task<PostagemResponseDTO> UpdateAsync(int id, PostagemUpdateDTO postUpdateDto, int usuarioLogadoId, bool ehAdmin)
    {
        if (id != postUpdateDto.PostagemId)
            throw new ArgumentException("Dados inválidos."); 

        var post = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == id);

        if (post is null)
            throw new KeyNotFoundException("Postagem não encontrada.");

        if (post.UsuarioId != usuarioLogadoId && !ehAdmin)
            throw new UnauthorizedAccessException("Sem permissão para editar a postagem.");

        post.UpdateToPost(postUpdateDto);

        var postAtualizado = _uof.PostagemRepository.Update(post);
        await _uof.CommitAsync();

        var postCompleto = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == postAtualizado.PostagemId);

        return postCompleto.ToPostResponseDTO();
    }

    public async Task DeleteAsync(int id, int usuarioLogadoId, bool ehAdmin)
    {
        var post = await _uof.PostagemRepository.GetAsync(p => p.PostagemId == id);

        if (post is null)
            throw new KeyNotFoundException("Postagem não encontrada.");

        if (post.UsuarioId != usuarioLogadoId && !ehAdmin)
            throw new UnauthorizedAccessException("Sem permissão para excluir a postagem.");

        _uof.PostagemRepository.Delete(post);
        await _uof.CommitAsync();
    }
}