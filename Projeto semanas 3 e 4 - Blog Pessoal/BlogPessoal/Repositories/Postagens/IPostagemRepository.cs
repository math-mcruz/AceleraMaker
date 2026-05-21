using BlogPessoal.Models;
using BlogPessoal.Models.Pagination;
using BlogPessoal.Repositories.GenericRepository;

namespace BlogPessoal.Repositories.Postagens;

public interface IPostagemRepository : IRepository<Postagem>
{
    Task<PagedResponse<Postagem>> GetFiltroAutorTemaAsync(PostagensFiltroAutorTema postFiltroParams);
}
