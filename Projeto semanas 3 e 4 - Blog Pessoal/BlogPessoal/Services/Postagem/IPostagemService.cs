using BlogPessoal.DTOs.Postagens;
using BlogPessoal.Models.Pagination;

namespace BlogPessoal.Services.Postagens;

public interface IPostagemService
{
    Task<IEnumerable<PostagemResponseDTO>> GetAllAsync();
    Task<PagedResponse<PostagemResponseDTO>> GetPorFiltroAsync(PostagensFiltroAutorTema postFiltro);
    Task<PostagemResponseDTO> CreateAsync(PostagemRequestDTO postRequestDto, int usuarioLogadoId);
    Task<PostagemResponseDTO> GerarResumoIAAsync(int postagemId);
    Task<PostagemResponseDTO> UpdateAsync(int id, PostagemUpdateDTO postUpdateDto, int usuarioLogadoId, bool ehAdmin);
    Task DeleteAsync(int id, int usuarioLogadoId, bool ehAdmin);
}
